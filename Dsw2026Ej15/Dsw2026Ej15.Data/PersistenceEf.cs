using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistance
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetActiveDoctorsAsync()
        {
            return _context.Doctors
                .Include(nameof(Doctor._speciality))
                .Where(d => d._isActive);
        }

        public async Task<Doctor?> GetActiveDoctorByIdAsync(Guid id)
        {
           return await _context.Doctors
                .Include(nameof(Doctor._speciality))
                .FirstOrDefaultAsync(d => d._id == id && d._isActive);
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateDoctorAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                doctor._isActive = false;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Speciality>> GetSpecialitiesAsync()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await _context.Specialities.SingleOrDefaultAsync(s => s._id == id);
        }
    }
}

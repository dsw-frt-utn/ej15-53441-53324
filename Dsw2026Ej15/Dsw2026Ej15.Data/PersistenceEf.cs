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
            return await _context.Doctors
                .Include(d => d._speciality)
                .Where(d => d._isActive)
                .ToListAsync();
        }

        public async Task<Doctor?> GetActiveDoctorByIdAsync(Guid id)
        {
            var doctor = await _context.Doctors
                .Include(d => d._speciality)
                .FirstOrDefaultAsync(d => d._id == id && d._isActive);
            if (doctor == null)
            {
                throw new NotFoundException($"Doctor with ID {id} not found.");
            }
            return doctor;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateDoctorAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new NotFoundException($"Doctor with ID {id} not found.");
            }
            doctor._isActive = false;
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Speciality>> GetSpecialitiesAsync()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality == null)
            {
                throw new NotFoundException($"Speciality with ID {id} not found.");
            }
            return speciality;
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistance
{
    private readonly MedicalContext _context;

    public PersistenceEf(MedicalContext context)
    {
        _context = context;
    }

    public List<Speciality> GetSpecialities()
    {
        return _context.Specialities.ToList();
    }

    public List<Doctor> GetDoctors()
    {
        return _context.Doctors.Include(d => d.Speciality).ToList();
    }

    public void SaveDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }
}


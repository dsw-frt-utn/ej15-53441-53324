using System.Collections.Generic;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain
{
    public interface IPersistance
    {
        List<Speciality> GetSpecialities();
        List<Doctor> GetDoctors();
        void SaveDoctor(Doctor doctor);
    }
} 
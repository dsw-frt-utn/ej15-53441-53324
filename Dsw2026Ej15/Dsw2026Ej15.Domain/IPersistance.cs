using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public interface IPersistance
    {
       
        Task<Speciality?> GetSpecialityByIdAsync(Guid id);
        Task AddDoctorAsync(Doctor doctor);
        Task<IEnumerable<Doctor>> GetActiveDoctorsAsync();
        Task<Doctor?> GetActiveDoctorByIdAsync(Guid id);
        Task DeactivateDoctorAsync(Guid id);
        //5 metodos
        //para el 16 hacerlo async

    }
}

using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using System.Text.Json;

namespace Dsw2026Ej15.Data;


public class PersistenceInMemory : IPersistance
{
    public PersistenceInMemory() { LoadSpecialities().GetAwaiter().GetResult(); } //uso el getawaiter porque no puedo usar directamente un metodo asincrono en un constructor. Entonces uso eso y despues get result pbloquea el hilo hasta que se complete la tarea asincrona.

    List<Doctor> Doctores = new List<Doctor>();
    List<Speciality> Especialidades = new List<Speciality>();

    public async Task LoadSpecialities()
    {
        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
        var json = await File.ReadAllTextAsync(jsonPath);
        var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(
        json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        try
        {
            if (specialities != null)
            {
                Especialidades = specialities.Select(dto => new Speciality
                {
                    _id = dto.Id,
                    _name = dto.Name,
                    _description = dto.Description
                }).ToList();
                Console.WriteLine("hola");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); throw; }

    }

    public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
    {
        var speciality = Especialidades.FirstOrDefault(e => e._id == id);
        return speciality;
    }

    public async Task AddDoctorAsync(Doctor doctor)
    {
        Doctores.Add(doctor);
    }

    public async Task<IEnumerable<Doctor>> GetActiveDoctorsAsync()
    {
        var activeDoctors = Doctores.Where(doctor => doctor._isActive).AsEnumerable();
        return activeDoctors;
    }

    public async Task<Doctor?> GetActiveDoctorByIdAsync(Guid id)
    {
        var doctor = Doctores.FirstOrDefault(d => d._id == id && d._isActive);
        return doctor;
    }

    public async Task DeactivateDoctorAsync(Guid id)
    {
        var doctor = Doctores.FirstOrDefault(d => d._id == id);
        if (doctor != null)
        {
            doctor._isActive = false;
        }
    }

}


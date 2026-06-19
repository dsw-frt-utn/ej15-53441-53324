using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using System.Text.Json;

namespace Dsw2026Ej15.Data;


    public class PersistenceInMemory : IPersistance
    {
    public PersistenceInMemory() { }

        List<Doctor> Doctores = new List<Doctor>();
        List<Speciality> Especialidades = new List<Speciality>();

    private async Task LoadSpecialities()
    {
        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "specialities.json");
        var specialities = JsonSerializer.Deserialize<List<Speciality>>(jsonPath);
    } 

    }


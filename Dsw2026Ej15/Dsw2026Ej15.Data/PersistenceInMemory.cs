using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistance
    {
        private readonly List<Speciality> _specialities = new List<Speciality>();
        private readonly List<Doctor> _doctors = new List<Doctor>();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public List<Speciality> GetSpecialities()
        {
            return _specialities;
        }

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }

        public void SaveDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        private void LoadSpecialities()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var jsonPath = Path.Combine(baseDirectory, "Sources", "specialities.json");

            if (!File.Exists(jsonPath)) return;

            var json = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var dtos = JsonSerializer.Deserialize<List<SpecialityDto>>(json, options);

            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    var spec = new Speciality();
                    spec.Id = dto.Id;
                    spec.Name = dto.Name;
                    _specialities.Add(spec);
                }
            }
        }
    }

    public record SpecialityDto(Guid Id, string Name, string Description);
} 
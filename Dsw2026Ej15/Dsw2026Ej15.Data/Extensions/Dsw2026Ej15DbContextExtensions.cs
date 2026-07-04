using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Extensions
{
    public static class Dsw2026Ej15DbContextExtensions
    {
        public static void SeedworkSpecialities(this Dsw2026Ej15DbContext context, string jsonFilePath)
        {

            context.Database.Migrate();
            if (context.Specialities.Any())
            {
                return;
            }

            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                var specialities = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                if (specialities != null && specialities.Any())
                {
                    context.Specialities.AddRange(specialities);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new FileNotFoundException($"No se encontró el archivo de datos iniciales en la ruta: {jsonFilePath}");
            }
        }
    }
    }

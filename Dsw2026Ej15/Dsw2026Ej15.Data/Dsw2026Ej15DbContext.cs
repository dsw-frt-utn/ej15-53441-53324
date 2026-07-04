using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class Dsw2026Ej15DbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public Dsw2026Ej15DbContext(DbContextOptions<Dsw2026Ej15DbContext> options) : base(options)
        {
 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d._id);

              
                entity.Property(d => d._name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(d => d._licenseNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                
                entity.Property(d => d._isActive)
                      .HasDefaultValue(true);

                entity.HasOne(d => d._speciality)
                      .WithMany()
                      .IsRequired(); 
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.HasKey(s => s._id);

                entity.Property(s => s._name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s._description)
                      .HasMaxLength(500);
            });
        }


    }
}

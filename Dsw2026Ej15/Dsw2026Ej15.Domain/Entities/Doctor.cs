using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string _name { get; set; }
        public string _licenseNumber { get; set; }
        public Speciality? _speciality { get; set; }
        public bool _isActive { get; set; }

        public Guid? SpecialityId { get; set; }

        private Doctor () { }

        public Doctor(string name, string licenseNumber, Speciality speciality)
        {
            _id = Guid.NewGuid();
            _name = name;
            _licenseNumber = licenseNumber;
            _speciality = speciality;
            _isActive = true;
        }
    }
}

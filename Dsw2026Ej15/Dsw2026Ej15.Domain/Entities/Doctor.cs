using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string _name { get; set; }
        public string _licenseNumber { get; set; }
        public Speciality _speciality { get; set; }
        public bool _isActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        string _name;
        string _LicenseNumber;
        Speciality _speciality;
        bool _isActive;
    }
}

using System;

namespace Dsw2026Ej15.Domain.Entities
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
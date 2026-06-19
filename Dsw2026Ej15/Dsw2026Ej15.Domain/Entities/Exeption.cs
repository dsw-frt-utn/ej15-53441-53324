using System;

namespace Dsw2026Ej15.Domain.Entities
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
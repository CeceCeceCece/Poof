using System;

namespace Application.Exceptions
{
    public class PoofException : Exception
    {
        public PoofException()
        {
        }

        public PoofException(string message) : base(message)
        {
        }

        public PoofException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

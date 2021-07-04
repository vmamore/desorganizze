using System;

namespace Desorganizze.Domain
{
    public class InvalidSizeException : Exception
    {
        public InvalidSizeException() {}

        public InvalidSizeException(string message) : base(message) {}
    }
}
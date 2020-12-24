using System;

namespace Desorganizze.Models
{
    public class InvalidSizeException : Exception
    {
        public InvalidSizeException()
        {
        }

        public InvalidSizeException(string message) : base(message)
        {
        }
    }
}
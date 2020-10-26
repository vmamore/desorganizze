using System;

namespace Desorganizze.Models
{
    public class MoneyCannotBeNegativeException : Exception
    {
        public MoneyCannotBeNegativeException()
        {
        }

        public MoneyCannotBeNegativeException(string message) : base(message)
        {
        }
    }
}
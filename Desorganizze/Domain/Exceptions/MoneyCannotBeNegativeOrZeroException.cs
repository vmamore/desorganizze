using System;

namespace Desorganizze.Domain
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
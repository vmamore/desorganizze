using System;

namespace Desorganizze.Models
{
    public class AccountCannotHaveNegativeBalance : Exception
    {
        public AccountCannotHaveNegativeBalance()
        {
        }

        public AccountCannotHaveNegativeBalance(string message) : base(message)
        {
        }
    }
}
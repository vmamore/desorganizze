using System;

namespace Desorganizze.Domain
{
    public class AccountCannotHaveNegativeBalance : Exception
    {
        public AccountCannotHaveNegativeBalance() {}

        public AccountCannotHaveNegativeBalance(string message) : base(message) {}
    }
}
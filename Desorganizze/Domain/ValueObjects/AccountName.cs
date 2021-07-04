using System;

namespace Desorganizze.Domain.ValueObjects
{
    public class AccountName
    {
        public virtual string Value { get; private set; }

        private AccountName() { }
        private AccountName(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            this.Value = value;
        }

        public static AccountName Create(string value) => new AccountName(value);

        public override bool Equals(object obj)
        {
            var accountName = obj as AccountName;

            if (accountName is null) return false;

            return this.Value.Equals(accountName.Value);
        }

        public override string ToString() => Value;
    }
}

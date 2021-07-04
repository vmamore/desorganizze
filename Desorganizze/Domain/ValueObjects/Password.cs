using System;

namespace Desorganizze.Domain.ValueObjects
{
    public class Password
    {
        private const int MAX_SIZE_ALLOWED = 256;
        public virtual string Value { get; private set; }
        private Password() { }
        private Password(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (value.Length > MAX_SIZE_ALLOWED)
                throw new InvalidSizeException($"{nameof(value)} cannot be bigger than {MAX_SIZE_ALLOWED}");

            this.Value = value;
        }

        public static Password Create(string password) => new Password(password);

        public override bool Equals(object obj)
        {
            var password = obj as Password;

            if (password is null) return false;

            return this.Value == password.Value;
        }

        public override string ToString() => Value;
    }
}

using System;

namespace Desorganizze.Domain.ValueObjects
{
    public class Username
    {
        private const int MAX_VALUE_ALLOWED = 256;
        public virtual string Value { get; private set; }
        private Username() { }
        private Username(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(value.Length > MAX_VALUE_ALLOWED)
                throw new InvalidSizeException($"{nameof(value)} cannot be bigger than {MAX_VALUE_ALLOWED}");

            this.Value = value;
        }

        public static Username Create(string username) => new Username(username);

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            var username = obj as Username;

            if (username is null) return false;

            return this.Value == username.Value;
        }
    }
}

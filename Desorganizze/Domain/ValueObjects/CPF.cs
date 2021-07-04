using System;

namespace Desorganizze.Domain.ValueObjects
{
    public class CPF
    {
        public virtual string Value { get; private set; }
        private CPF() { }
        private CPF(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (!IsValid(value))
                throw new ArgumentException(nameof(value));

            this.Value = value;
        }

        public static CPF Create(string cpf) => new CPF(cpf);

        private bool IsValid(string value) => true;

        public override string ToString() => Value;
    }
}
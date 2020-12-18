using System;

namespace Desorganizze.Models.ValueObjects
{
    public class CPF
    {
        public virtual string Valor { get; private set; }
        private CPF() { }
        private CPF(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ArgumentNullException(nameof(valor));

            this.Valor = valor;
        }

        public static CPF Create(string cpf)
        {
            return new CPF(cpf);
        }

        public override string ToString() => Valor;
    }
}
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

            if (!EhValido(valor))
                throw new ArgumentException(nameof(valor));

            this.Valor = valor;
        }

        public static CPF Create(string cpf)
        {
            return new CPF(cpf);
        }

        private bool EhValido(string valor) => true;

        public override string ToString() => Valor;
    }
}
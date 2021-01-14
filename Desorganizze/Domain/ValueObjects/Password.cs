using System;

namespace Desorganizze.Domain.ValueObjects
{
    public class Password
    {
        private const int TAMANHO_MAXIMO_PERMITIDO = 256;
        public virtual string Valor { get; private set; }
        private Password() { }
        private Password(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ArgumentNullException(nameof(valor));

            if (valor.Length > TAMANHO_MAXIMO_PERMITIDO)
                throw new InvalidSizeException($"{nameof(valor)} cannot be bigger than {TAMANHO_MAXIMO_PERMITIDO}");

            this.Valor = valor;
        }

        public static Password Create(string password) => new Password(password);

        public override bool Equals(object obj)
        {
            var password = obj as Password;

            if (password is null) return false;

            return this.Valor == password.Valor;
        }

        public override string ToString() => Valor;
    }
}

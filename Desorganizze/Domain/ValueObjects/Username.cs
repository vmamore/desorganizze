using System;

namespace Desorganizze.Domain.ValueObjects
{
    public class Username
    {
        private const int TAMANHO_MAXIMO_PERMITIDO = 256;
        public virtual string Valor { get; private set; }
        private Username() { }
        private Username(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ArgumentNullException(nameof(valor));

            if(valor.Length > TAMANHO_MAXIMO_PERMITIDO)
                throw new InvalidSizeException($"{nameof(valor)} cannot be bigger than {TAMANHO_MAXIMO_PERMITIDO}");

            this.Valor = valor;
        }

        public static Username Create(string username) => new Username(username);

        public override string ToString() => Valor;

        public override bool Equals(object obj)
        {
            var username = obj as Username;

            if (username is null) return false;

            return this.Valor == username.Valor;
        }
    }
}

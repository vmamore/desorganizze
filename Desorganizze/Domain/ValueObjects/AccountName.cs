using System;

namespace Desorganizze.Domain.ValueObjects
{
    public class AccountName
    {
        public virtual string Valor { get; private set; }

        private AccountName() { }
        private AccountName(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ArgumentNullException(nameof(valor));

            this.Valor = valor;
        }

        public static AccountName Create(string valor) => new AccountName(valor);

        public override bool Equals(object obj)
        {
            var accountName = obj as AccountName;

            if (accountName is null) return false;

            return this.Valor.Equals(accountName.Valor);
        }

        public override string ToString() => Valor;
    }
}

using Desorganizze.Infra.CQRS.Commands;
using System;

namespace Desorganizze.Application.Commands.Wallets
{
    public class CreateAccount : ICommand
    {
        public CreateAccount(string walletId, string name)
        {
            WalletId = new Guid(walletId);
            Name = name;
        }

        public Guid WalletId { get; }
        public string Name { get; }
    }
}

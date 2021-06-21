namespace Desorganizze.Application.Commands.Wallets
{
    using Desorganizze.Infra.CQRS.Commands;
    using System;

    public class CreateCategory : ICommand
    {
        public string Description { get; }
        public Guid WalletId { get; }

        public CreateCategory(string walletId, string description)
        {
            WalletId = Guid.Parse(walletId);
            Description = description;
        }
    }
}

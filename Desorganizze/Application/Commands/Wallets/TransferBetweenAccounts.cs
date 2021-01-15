using Desorganizze.Infra.CQRS.Commands;
using System;

namespace Desorganizze.Application.Commands.Wallets
{
    public class TransferBetweenAccounts : ICommand
    {
        public TransferBetweenAccounts(string walletId, Guid sourceAccountId, Guid recipientAccountId, decimal amount)
        {
            WalletId = new Guid(walletId);
            SourceAccountId = sourceAccountId;
            RecipientAccountId = recipientAccountId;
            Amount = amount;
        }

        public Guid WalletId { get; }
        public Guid SourceAccountId { get;  }
        public Guid RecipientAccountId { get;  }
        public decimal Amount { get; }
    }
}

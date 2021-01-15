using Desorganizze.Domain;
using Desorganizze.Infra.CQRS.Commands;
using System;

namespace Desorganizze.Application.Commands.Wallets
{
    public class CreateTransaction : ICommand
    {
        public CreateTransaction(Guid accountId, decimal amount, int type)
        {
            AccountId = accountId;
            Amount = amount;
            Type = (TransactionType) type;
        }

        public Guid AccountId { get; }
        public decimal Amount { get; }
        public TransactionType Type { get; }

    }
}

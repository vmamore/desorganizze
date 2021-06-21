using Desorganizze.Domain;
using Desorganizze.Infra.CQRS.Commands;
using System;

namespace Desorganizze.Application.Commands.Wallets
{
    public class CreateTransaction : ICommand
    {
        public CreateTransaction(Guid accountId, Guid categoryId, decimal amount, int type)
        {
            AccountId = accountId;
            Amount = amount;
            Type = (TransactionType) type;
            CategoryId = categoryId;
        }

        public Guid AccountId { get; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; }
        public TransactionType Type { get; }

    }
}

using Desorganizze.Domain;
using System;

namespace Desorganizze.Application.Queries.Wallets.ReadModel
{

    public class TransactionQueryDto
    {
        public decimal Amount { get; }
        public string Type { get; }
        public string AccountName { get; }
        public DateTime CreatedDate { get; }

        public TransactionQueryDto(decimal amount, TransactionType type, DateTime createdDate, string accountName)
        {
            Amount = amount;
            Type = type == TransactionType.Add ? "ADD" : "SUBTRACT";
            CreatedDate = createdDate;
            AccountName = accountName;
        }
    }
}

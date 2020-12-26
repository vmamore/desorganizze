using Desorganizze.Models;
using System;

namespace Desorganizze.Dtos
{
    public class TransactionsQueryDto
    {
        public decimal TotalAmount { get; }
    }

    public class TransactionQueryDto
    {
        public decimal Amount { get; }
        public string Type { get; }
        public string AccountName { get; set; }
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

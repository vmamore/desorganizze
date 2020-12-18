using Desorganizze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public DateTime CreatedDate { get; }

        public TransactionQueryDto(decimal amount, TransactionType type, DateTime createdDate)
        {
            Amount = amount;
            Type = type == TransactionType.Add ? "ADD" : "SUBTRACT";
            CreatedDate = createdDate; 
        }
    }
}

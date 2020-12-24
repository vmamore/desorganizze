﻿using Desorganizze.Models.ValueObjects;
using System;

namespace Desorganizze.Models
{
    public enum TransactionType
    {
        Add,
        Subtract
    }

    public class Transaction
    {
        public virtual Guid Id { get; }
        public virtual TransactionType Type { get; }
        public virtual DateTime CreatedDate { get; }
        public virtual Money TotalAmount { get; }
        public virtual Account Account { get; protected set; }
        private bool IsAdding => Type == TransactionType.Add;

        protected Transaction() {}

        private Transaction(decimal totalAmount, TransactionType type, Account account)
        {
            Id = Guid.NewGuid();
            Type = type;
            CreatedDate = DateTime.UtcNow;
            TotalAmount = Money.Create(totalAmount);
            Account = account;
        }

        public static Transaction CreateTransactionFromType(decimal totalAmount, Account account, TransactionType type)
            => type switch
            {
                TransactionType.Add => new Transaction(totalAmount, TransactionType.Add, account),
                TransactionType.Subtract => new Transaction(totalAmount, TransactionType.Subtract, account),
                _ => throw new InvalidOperationException("Transaction type not defined")
            };

        public virtual decimal GetAmountByType() => this.IsAdding ? this.TotalAmount.Amount : -this.TotalAmount.Amount;
    }
}

﻿using System;

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
        public virtual bool IsAdding => Type == TransactionType.Add;

        protected Transaction() {}

        public Transaction(int totalAmount, TransactionType type, Account account)
        {
            Id = Guid.NewGuid();
            Type = type;
            CreatedDate = DateTime.UtcNow;
            TotalAmount = Money.Create(totalAmount);
            Account = account;
        }
    }
}
using System;

namespace Desorganizze.Models
{
    public enum Type
    {
        Add,
        Subtract
    }

    public class Transaction
    {
        public virtual Guid Id { get; }
        public virtual Type Type { get; }
        public virtual DateTime CreatedDate { get; }
        public virtual Money TotalAmount { get; }
        public virtual Account Account { get; private set; }
        public virtual bool IsAdding => Type == Type.Add;

        protected Transaction() {}

        public Transaction(int totalAmount, Type type, Account account)
        {
            Id = Guid.NewGuid();
            Type = type;
            CreatedDate = DateTime.UtcNow;
            TotalAmount = Money.Create(totalAmount);
            Account = account;
        }
    }
}

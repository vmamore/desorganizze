using Desorganizze.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Desorganizze.Models
{
    public class Account
    {
        public virtual Guid Id { get; protected set; }
        public virtual User User { get; protected set; }
        private readonly IList<Transaction> _transactions;
        public virtual IReadOnlyCollection<Transaction> Transactions => new ReadOnlyCollection<Transaction>(_transactions);

        public virtual Money GetBalance
        {
            get
            {
                return _transactions
                    .OrderBy(t => t.CreatedDate)
                    .Aggregate(Money.Zero(), (current, credit) =>
                    Money.Create(current.Amount + credit.GetAmountByType()));
            }
        }

        protected Account() { }

        public Account(User user)
        {
            this.Id = Guid.NewGuid();
            _transactions = new List<Transaction>();
            User = user;
        }

        public virtual Transaction NewTransaction(decimal amount, TransactionType type)
        {
            var transaction = new Transaction(amount, type, this);

            var totalBalance = GetBalance.Amount + transaction.GetAmountByType();

            if (totalBalance < 0)
                throw new AccountCannotHaveNegativeBalance();

            _transactions.Add(transaction);

            return transaction;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Desorganizze.Models
{
    public class Account
    {
        public virtual int Id { get; private set; }
        public virtual User User { get; private set; }
        private List<Transaction> _transactions;
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        public Money GetBalance
        {
            get
            {
                var total = Money.Create(0);

                return _transactions.Aggregate(total, (current, credit) =>
                    Money.Create(
                    credit.IsAdding ? credit.TotalAmount.Amount : -credit.TotalAmount.Amount
                    + current.Amount));
            }
        }

        public Account(User user)
        {
            User = user;
        }

        public void NewTransaction(int amount, Type type)
        {
            var transaction = new Transaction(amount, type, this);

            var totalBalance = GetBalance + transaction.TotalAmount;

            if (totalBalance.Amount < 0)
                throw new AccountCannotHaveNegativeBalance();

            _transactions.Add(transaction);
        }
    }
}

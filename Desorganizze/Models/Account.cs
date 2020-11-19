using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Desorganizze.Models
{
    public class Account
    {
        public virtual int Id { get; protected set; }
        public virtual int UserId { get; protected set; }
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
                    Money.Create(
                    credit.IsAdding ? credit.TotalAmount.Amount : -credit.TotalAmount.Amount
                    + current.Amount));
            }
        }

        protected Account() { }

        public Account(User user)
        {
            _transactions = new List<Transaction>();
            User = user;
        }

        public virtual Transaction NewTransaction(decimal amount, TransactionType type)
        {
            var transaction = new Transaction(amount, type, this);

            var totalBalance = GetBalance + transaction.TotalAmount;

            if (totalBalance.Amount < 0)
                throw new AccountCannotHaveNegativeBalance();

            _transactions.Add(transaction);

            return transaction;
        }
    }
}

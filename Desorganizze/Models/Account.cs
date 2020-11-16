using System.Collections.Generic;
using System.Linq;

namespace Desorganizze.Models
{
    public class Account
    {
        public virtual int Id { get; protected set; }
        public virtual int UserId { get; protected set; }
        public virtual User User { get; protected set; }
        private readonly IList<Transaction> _transactions;
        public virtual IReadOnlyCollection<Transaction> Transactions { get; protected set; }

        public virtual Money GetBalance
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

        public Account() {}

        public Account(User user)
        {
            _transactions = new List<Transaction>();
            Transactions = (_transactions as List<Transaction>).AsReadOnly();
            User = user;
        }

        public virtual Transaction NewTransaction(int amount, TransactionType type)
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

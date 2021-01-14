using Desorganizze.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Desorganizze.Domain
{
    public class Account
    {
        public virtual Guid Id { get; protected set; }
        public virtual AccountName Name { get; set; }
        public virtual Wallet Wallet { get; protected set; }
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

        public Account(Wallet wallet, string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = AccountName.Create(name);
            _transactions = new List<Transaction>();
            Wallet = wallet;
        }

        public virtual Transaction NewTransaction(decimal amount, TransactionType type)
        {
            var transaction = Transaction.CreateTransactionFromType(amount, this, type);

            var totalBalanceWithNewTransaction = GetBalance.Amount + transaction.GetAmountByType();

            if (0 > totalBalanceWithNewTransaction)
                throw new AccountCannotHaveNegativeBalance();

            _transactions.Add(transaction);

            return transaction;
        }

        public virtual void CreateDebitTransaction(decimal totalValue)
        {
            NewTransaction(totalValue, TransactionType.Subtract);
        }

        public virtual void CreateCreditTransaction(decimal totalValue)
        {
            NewTransaction(totalValue, TransactionType.Add);
        }
    }
}

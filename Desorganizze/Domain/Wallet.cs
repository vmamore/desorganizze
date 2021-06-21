using Desorganizze.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Desorganizze.Domain
{
    public class Wallet
    {
        public virtual Guid Id { get; protected set; }
        public virtual User User { get; protected set; }

        private readonly IList<Account> _accounts;
        public virtual IReadOnlyCollection<Account> Accounts => new ReadOnlyCollection<Account>(_accounts);
        private readonly IList<Category> _categories;
        public virtual IReadOnlyCollection<Category> Categories => new ReadOnlyCollection<Category>(_categories);
        protected Wallet() {}

        public Wallet(User user)
        {
            User = user;
            _accounts = new List<Account>();
            _categories = new List<Category>();
        }

        public virtual Account NewAccount(string accountName)
        {
            var account = new Account(this, accountName);

            _accounts.Add(account);

            return account;
        }

        public virtual Money GetBalance()
        {
            return Money.Zero();
        }

        public virtual void TransferBetweenAccounts(Guid recipientAccountId, Guid sourceAccountId, decimal totalValue)
        {
            var recipientAccount = _accounts.SingleOrDefault(acc => acc.Id == recipientAccountId);

            if (recipientAccount is null)
                throw new ArgumentNullException($"Recipient account not found: {recipientAccount}");

            var sourceAccount = _accounts.SingleOrDefault(acc => acc.Id == sourceAccountId);

            if (sourceAccount is null)
                throw new ArgumentNullException($"Source account not found: {sourceAccount}");

            sourceAccount.CreateDebitTransaction(totalValue);

            recipientAccount.CreateCreditTransaction(totalValue);
        }
        public virtual void CreateNewCategory(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description), "Category descrption cannot be null or empty.");

            _categories.Add(new Category(description, this));
        }
    }
}

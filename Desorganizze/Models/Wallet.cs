using Desorganizze.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Desorganizze.Models
{
    public class Wallet
    {
        public virtual Guid Id { get; protected set; }
        public virtual User User { get; protected set; }

        private readonly IList<Account> _accounts;
        public virtual IReadOnlyCollection<Account> Accounts => new ReadOnlyCollection<Account>(_accounts);

        protected Wallet() {}

        public Wallet(User user)
        {
            User = user;
            _accounts = new List<Account>();
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
    }
}

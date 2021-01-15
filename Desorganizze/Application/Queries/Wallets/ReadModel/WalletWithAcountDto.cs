using System;
using System.Collections.Generic;

namespace Desorganizze.Application.Queries.Wallets.ReadModel
{
    public class WalletWithAcountDto
    {
        public Guid WalletId { get; set; }
        public IEnumerable<AccountDto> Accounts { get; set; }
    }

    public class AccountDto
    {
        public Guid AccountId { get; set; }
        public string Namme { get; set; }
    }
}

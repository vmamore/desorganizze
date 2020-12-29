using System;
using System.Collections.Generic;

namespace Desorganizze.Dtos
{
    public class GetWalletByUserId
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

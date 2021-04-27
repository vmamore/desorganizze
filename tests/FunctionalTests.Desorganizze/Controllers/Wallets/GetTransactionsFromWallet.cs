using System;

namespace FunctionalTests.Desorganizze.Controllers.Wallets
{
    public class GetTransactionsFromWallet
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string AccountName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

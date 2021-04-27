using System;

namespace FunctionalTests.Desorganizze.Controllers.Wallets
{
    public class PostNewTransactionDto
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public int TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}

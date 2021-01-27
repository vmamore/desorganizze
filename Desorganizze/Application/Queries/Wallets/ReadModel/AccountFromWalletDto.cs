using System;

namespace Desorganizze.Application.Queries.Wallets.ReadModel
{
    public class AccountFromWalletDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; }
        public string Name { get; }

        public AccountFromWalletDto(Guid id, decimal totalAmount, string name)
        {
            Id = id;
            TotalAmount = totalAmount;
            Name = name;
        }
    }
}

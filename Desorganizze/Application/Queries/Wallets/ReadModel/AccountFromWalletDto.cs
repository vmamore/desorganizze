namespace Desorganizze.Application.Queries.Wallets.ReadModel
{
    public class AccountFromWalletDto
    {
        public decimal TotalAmount { get; }
        public string Name { get; }

        public AccountFromWalletDto(decimal totalAmount, string name)
        {
            TotalAmount = totalAmount;
            Name = name;
        }
    }
}

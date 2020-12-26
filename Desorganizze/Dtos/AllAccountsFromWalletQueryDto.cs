namespace Desorganizze.Dtos
{
    public class AllAccountsFromWalletQueryDto
    {
        public decimal TotalAmount { get; }
        public string Name { get; }

        public AllAccountsFromWalletQueryDto(decimal totalAmount, string name)
        {
            TotalAmount = totalAmount;
            Name = name;
        }
    }
}

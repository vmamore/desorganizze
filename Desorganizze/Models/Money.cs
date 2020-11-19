namespace Desorganizze.Models
{
    public class Money
    {
        public virtual decimal Amount { get; private set; }

        private Money() {}
        private Money(decimal amount)
        {
            if (amount < 0)
                throw new MoneyCannotBeNegativeException();

            Amount = amount;
        }

        public static Money Create(decimal totalValue) => new Money(totalValue);

        public static Money operator +(Money m1, Money m2) => Money.Create(m1.Amount + m2.Amount);
        public static Money operator -(Money m1, Money m2) => Money.Create(m1.Amount - m2.Amount);

        public override string ToString() => $"R$ {Amount}";
    }
}

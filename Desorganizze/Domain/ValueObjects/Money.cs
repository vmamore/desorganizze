using System;

namespace Desorganizze.Domain.ValueObjects
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
        public static Money Zero() => new Money(0);

        public static Money operator +(Money m1, Money m2) => Money.Create(m1.Amount + m2.Amount);
        public static Money operator -(Money m1, Money m2) => Money.Create(m1.Amount - m2.Amount);
        public bool IsZero() => this.Amount == 0;

        public override string ToString() => $"R$ {Amount}";

        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount.Equals(other.Amount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Money)obj);
        }
        public override int GetHashCode() => Amount.GetHashCode();

        public static bool operator ==(Money left, Money right) =>
        Equals(left, right);

        public static bool operator !=(Money left, Money right) =>
        !Equals(left, right);
    }
}

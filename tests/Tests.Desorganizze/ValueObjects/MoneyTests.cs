using AutoFixture;
using Desorganizze.Models;
using Desorganizze.Models.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTests.Desorganizze.ValueObjects
{
    public class MoneyTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Should_create_valid_Money()
        {
            var amount = _fixture.Create<decimal>();

            var money = Money.Create(amount);

            money.Amount.Should().Be(amount);
        }

        [Fact]
        public void Should_throw_exception_when_amount_is_negative()
        {
            var negativeAmount = -_fixture.Create<decimal>();

            Action expectedException = () => Money.Create(negativeAmount);

            expectedException.Should().Throw<MoneyCannotBeNegativeException>();
        }
    }
}

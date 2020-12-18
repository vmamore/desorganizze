using AutoFixture;
using Desorganizze.Models;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTests.Desorganizze.Models
{
    public class AccountTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Should_create_valid_account()
        {
            var user = _fixture.Create<User>();

            var account = new Account(user);

            account.User.Should().Be(user);
        }

        [Theory]
        [InlineData(1000.0, TransactionType.Add)]
        public void Should_create_valid_transaction(decimal amount, TransactionType type)
        {
            var user = _fixture.Create<User>();
            var account = new Account(user);

            var transactionCreated = account.NewTransaction(amount, type);

            transactionCreated.TotalAmount.Amount.Should().Be(amount);
            transactionCreated.Type.Should().Be(type);
        }

        [Fact]
        public void Should_return_the_right_balace()
        {
            var user = _fixture.Create<User>();
            var account = new Account(user);

            var firstTransaction = account.NewTransaction(_fixture.Create<decimal>(), TransactionType.Add);
            var secondTransaction = account.NewTransaction(_fixture.Create<decimal>(), TransactionType.Add);
            var totalAmountFromTransactions = firstTransaction.TotalAmount + secondTransaction.TotalAmount;

            account.GetBalance.Should().Be(totalAmountFromTransactions);
        }

        [Fact]
        public void Should_throw_exception_when_balance_is_less_than_zero()
        {
            var user = _fixture.Create<User>();
            var account = new Account(user);

            Action action = () => account.NewTransaction(_fixture.Create<decimal>(), TransactionType.Subtract);

            action.Should().Throw<AccountCannotHaveNegativeBalance>();
        }
    }
}

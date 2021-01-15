using AutoFixture;
using Desorganizze.Domain;
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
            var wallet = _fixture.Create<Wallet>();
            var accountName = _fixture.Create<string>();
            var account = new Account(wallet, accountName);

            account.Wallet.Should().Be(wallet);
        }

        [Theory]
        [InlineData(1000.0, TransactionType.Add)]
        public void Should_create_valid_transaction(decimal amount, TransactionType type)
        {
            var wallet = _fixture.Create<Wallet>();
            var accountName = _fixture.Create<string>();
            var account = new Account(wallet, accountName);

            var transactionCreated = account.NewTransaction(amount, type);

            transactionCreated.TotalAmount.Amount.Should().Be(amount);
            transactionCreated.Type.Should().Be(type);
        }

        [Fact]
        public void Should_return_the_right_balace()
        {
            var wallet = _fixture.Create<Wallet>();
            var accountName = _fixture.Create<string>();
            var account = new Account(wallet, accountName);

            var firstTransaction = account.NewTransaction(_fixture.Create<decimal>(), TransactionType.Add);
            var secondTransaction = account.NewTransaction(_fixture.Create<decimal>(), TransactionType.Add);
            var totalAmountFromTransactions = firstTransaction.TotalAmount + secondTransaction.TotalAmount;

            account.GetBalance.Should().Be(totalAmountFromTransactions);
        }

        [Fact]
        public void Should_throw_exception_when_balance_is_less_than_zero()
        {
            var wallet = _fixture.Create<Wallet>();
            var accountName = _fixture.Create<string>();
            var account = new Account(wallet, accountName);

            Action action = () => account.NewTransaction(_fixture.Create<decimal>(), TransactionType.Subtract);

            action.Should().Throw<AccountCannotHaveNegativeBalance>();
        }
    }
}

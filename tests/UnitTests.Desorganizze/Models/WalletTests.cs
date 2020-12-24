using AutoFixture;
using Desorganizze.Models;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using UnitTests.Desorganizze.Extensions;
using System;

namespace UnitTests.Desorganizze.Models
{
    public class WalletTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Should_create_valid_wallet()
        {
            var user = _fixture.Create<User>();

            var wallet = new Wallet(user);

            wallet.User.Should().Be(user);
        }

        [Fact]
        public void Should_create_valid_account()
        {
            var accountName = _fixture.Create<string>();
            var wallet = _fixture.Create<Wallet>();

            var account = wallet.NewAccount(accountName);

            account.Wallet.Should().Be(wallet);
            account.Name.Valor.Should().Be(accountName);
        }

        [Fact]
        public void When_transfering_money_between_accounts_should_throw_exception_when_recipient_account_is_not_found()
        {
            var recipientAccountId = Guid.NewGuid();
            var valueToTransfer = _fixture.Create<decimal>();
            var wallet = _fixture.Create<Wallet>();

            Action action = () => wallet.TransferBetweenAccounts(
                recipientAccountId: recipientAccountId,
                sourceAccountId: Guid.NewGuid(),
                totalValue: valueToTransfer);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void When_transfering_money_between_accounts_should_throw_exception_when_source_account_is_not_found()
        {
            
            var valueToTransfer = _fixture.Create<decimal>();
            var recipientAccount = _fixture.Create<Account>();
            var wallet = _fixture.Create<Wallet>();
            wallet.Set("_accounts", new List<Account>
            {
                recipientAccount
            });

            Action action = () => wallet.TransferBetweenAccounts(
                recipientAccountId: recipientAccount.Id,
                sourceAccountId: Guid.NewGuid(),
                totalValue: valueToTransfer);

            action.Should().Throw<ArgumentNullException>();
        }


        [Fact]
        public void Should_transfer_money_between_acounts_from_the_same_wallet()
        {
            var valueToTransfer = 1_000m;
            var sourceAccount = _fixture.Create<Account>();
            var transactionInSourceAccount = Transaction.CreateTransactionFromType(10_000m, sourceAccount, TransactionType.Add);
            var recipientAccount = _fixture.Create<Account>();
            var wallet = _fixture.Create<Wallet>();
            wallet.Set("_accounts", new List<Account>
            {
                sourceAccount,
                recipientAccount
            });
            sourceAccount.Set("_transactions", new List<Transaction>
            {
                transactionInSourceAccount
            });

            wallet.TransferBetweenAccounts(
                recipientAccountId: recipientAccount.Id,
                sourceAccountId: sourceAccount.Id,
                totalValue: valueToTransfer);

            sourceAccount.GetBalance.Amount.Should().Be(transactionInSourceAccount.TotalAmount.Amount - valueToTransfer);
            recipientAccount.GetBalance.Amount.Should().Be(valueToTransfer);
        }
    }
}

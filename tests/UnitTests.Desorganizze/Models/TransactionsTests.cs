using AutoFixture;
using Desorganizze.Models;
using FluentAssertions;
using Xunit;

namespace UnitTests.Desorganizze.Models
{
    public class TransactionsTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Should_create_valid_transaction()
        {
            var totalAmount = _fixture.Create<decimal>();
            var transactionType = _fixture.Create<TransactionType>();
            var account = _fixture.Create<Account>();

            var transaction = Transaction.CreateTransactionFromType(totalAmount, account, transactionType);

            transaction.Account.Should().Be(account);
            transaction.Type.Should().Be(transactionType);
            transaction.TotalAmount.Amount.Should().Be(totalAmount);
        }
    }
}

using AutoFixture;
using Desorganizze.Models;
using FluentAssertions;
using Xunit;

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
    }
}

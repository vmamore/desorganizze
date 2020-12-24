using System;
using Xunit;
using FluentAssertions;
using AutoFixture;
using Desorganizze.Models.ValueObjects;

namespace UnitTests.Desorganizze.ValueObjects
{
    public class AccountNameTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Should_create_valid_AccountName()
        {
            var name = _fixture.Create<string>();

            var accountName = AccountName.Create(name);

            name.ToString().Should().Be(name);
        }

        [Fact]
        public void Should_throw_exception_when_value_is_null()
        {
            Action expectedException = () => AccountName.Create(null);

            expectedException.Should().Throw<ArgumentNullException>();
        }
    }
}

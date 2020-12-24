using Bogus;
using System;
using Xunit;
using FluentAssertions;
using Desorganizze.Models.ValueObjects;
using Desorganizze.Models;

namespace UnitTests.Desorganizze.ValueObjects
{
    public class PasswordTests
    {
        private Faker _faker = new Faker();

        [Fact]
        public void Should_create_valid_Password()
        {
            var passwordValue = _faker.Random.String(256);

            var password = Password.Create(passwordValue);

            password.ToString().Should().Be(passwordValue);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_throw_exception_when_value_is_null(string passwordValue)
        {
            Action expectedException = () => Password.Create(passwordValue);

            expectedException.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_exception_when_value_is_bigger_than_allowed()
        {
            var passwordValue = _faker.Random.String(257);

            Action expectedException = () => Password.Create(passwordValue);

            expectedException.Should().Throw<InvalidSizeException>()
                             .Where(e => e.Message.Contains($"cannot be bigger than"));
        }
    }
}

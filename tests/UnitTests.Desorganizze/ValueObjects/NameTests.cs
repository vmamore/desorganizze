using Bogus;
using System;
using Xunit;
using FluentAssertions;
using Desorganizze.Domain.ValueObjects;

namespace UnitTests.Desorganizze.ValueObjects
{
    public class NameTests
    {
        private Faker _faker = new Faker();

        [Fact]
        public void Should_create_valid_Name()
        {
            var firstName = _faker.Person.FirstName;
            var lastName = _faker.Person.LastName;

            var name = Name.Create(firstName, lastName);

            name.ToString().Should().Be($"{firstName} {lastName}");
        }

        [Theory]
        [InlineData("Vinicius", null)]
        [InlineData(null, "Vinicius")]
        public void Should_throw_exception_when_value_is_null(string firstName, string lastName)
        {
            Action expectedException = () => Name.Create(firstName, lastName);

            expectedException.Should().Throw<ArgumentNullException>();
        }
    }
}

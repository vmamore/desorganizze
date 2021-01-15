using Bogus;
using System;
using Xunit;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Desorganizze.Domain.ValueObjects;

namespace UnitTests.Desorganizze.ValueObjects
{
    public class CPFTests
    {
        private Faker _faker = new Faker();

        [Fact]
        public void Should_create_valid_CPF()
        {
            var cpfValue = _faker.Person.Cpf();

            var cpf = CPF.Create(cpfValue);

            cpf.Valor.Should().Be(cpfValue);
        }

        [Fact]
        public void Should_throw_exception_when_value_is_nul()
        {
            Action expectedException = () => CPF.Create(null);

            expectedException.Should().Throw<ArgumentNullException>();
        }
    }
}

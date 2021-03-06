﻿using AutoFixture;
using Xunit;
using Bogus.Extensions.Brazil;
using Bogus;
using FluentAssertions;
using Desorganizze.Domain;

namespace UnitTests.Desorganizze.Models
{
    public class UserTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Should_create_valid_user()
        {
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var username = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var cpf = new Faker().Person.Cpf();

            var user = new User(firstName, lastName, cpf, username, password);

            user.Name.FirstName.Should().Be(firstName);
            user.Name.LastName.Should().Be(lastName);
            user.CPF.Value.Should().Be(cpf);
            user.Password.Value.Should().Be(password);
            user.Username.Value.Should().Be(username);
        }
    }
}

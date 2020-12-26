using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze.Controllers.Login
{
    [Collection("Server collection")]
    public class LoginControllerTests : IntegrationTest
    {
        public LoginControllerTests(ServerFixture serverFixture) : base(serverFixture) {}

        [Fact]
        public async Task Should_Return_OK_And_Token_When_Valid_User_Is_Trying_To_login()
        {
            var model = new
            {
                username = "vmamore",
                password = "teste123"
            };

            var response = await PostAsync("/api/login", model);
            var responseDeserialized = await DeserializeAsync<LoginPostResponseDto>(response);

            response.EnsureSuccessStatusCode();

            responseDeserialized.Username.Should().NotBeNullOrEmpty();
            responseDeserialized.Cpf.Should().NotBeNullOrEmpty();
            responseDeserialized.Name.Should().NotBeNullOrEmpty();
            responseDeserialized.WalletId.Should().NotBeNullOrEmpty();
            responseDeserialized.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Should_Return_NotFound_When_User_Is_Not_Valid()
        {
            var model = new
            {
                username = "vmamore",
                password = "senhainvalida"
            };

            var response = await PostAsync("/api/login", model);
            var responseMessage = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseMessage.Should().Be($"{model.username} não existe.");
        }


        [Theory]
        [InlineData("", "senha")]
        [InlineData("username", "")]
        [InlineData("", "")]
        public async Task Should_Return_BadRequest_When_User_Is_Not_Valid(string username, string password)
        {
            var model = new
            {
                username = username,
                password = password
            };

            var response = await PostAsync("/api/login", model);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

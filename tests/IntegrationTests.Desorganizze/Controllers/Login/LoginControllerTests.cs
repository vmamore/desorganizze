using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze.Controllers.Login
{
    [Collection("Server collection")]
    public class LoginControllerTests
    {
        private ServerFixture _server;
        public LoginControllerTests(ServerFixture serverFixture)
        {
            _server = serverFixture;
        }

        [Fact]
        public async Task Should_Return_OK_And_Token_When_Valid_User_Is_Trying_To_login()
        {
            var model = new
            {
                username = "vmamore",
                password = "teste123"
            };
            var bodyRequest = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _server.Client.PostAsync("/api/login", bodyRequest);
            var responseDeserialized = await JsonSerializer.DeserializeAsync<LoginPostResponseDto>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
            var bodyRequest = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _server.Client.PostAsync("/api/login", bodyRequest);

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
            var bodyRequest = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _server.Client.PostAsync("/api/login", bodyRequest);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

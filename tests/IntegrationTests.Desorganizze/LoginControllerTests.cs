using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze
{
    public class LoginResponseDto
    {
        public string Username { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string WalletId { get; set; }
        public string Token { get; set; }
    }
    public class LoginControllerTests : IClassFixture<ServerFixture>
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
            var responseDeserialized = await JsonSerializer.DeserializeAsync<LoginResponseDto>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            response.EnsureSuccessStatusCode();

            responseDeserialized.Username.Should().NotBeNullOrEmpty();
            responseDeserialized.Cpf.Should().NotBeNullOrEmpty();
            responseDeserialized.Name.Should().NotBeNullOrEmpty();
            responseDeserialized.WalletId.Should().NotBeNullOrEmpty();
            responseDeserialized.Token.Should().NotBeNullOrEmpty();
        }
    }
}

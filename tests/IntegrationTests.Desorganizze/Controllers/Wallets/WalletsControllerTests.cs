using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze.Controllers.Wallets
{
    [Collection("Server collection")]
    public class WalletsControllerTests : IntegrationTest
    {
        public WalletsControllerTests(ServerFixture serverFixture) : base(serverFixture) {}

        [Fact]
        public async Task Should_Get_Wallet_From_Valid_UserId()
        {
            var userId = 1;
            var token = await GetTokenAsync();

            var response = await GetAsync($"wallets/{userId}/user", token);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_Get_BadRequest_From_Invalid_UserId()
        {
            var userId = 0;
            var token = await GetTokenAsync();

            var response = await GetAsync($"wallets/{userId}/user", token);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Get_NotFound_From_Inexistent_UserId()
        {
            var userId = -1;
            var token = await GetTokenAsync();

            var response = await GetAsync($"wallets/{userId}/user", token);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}

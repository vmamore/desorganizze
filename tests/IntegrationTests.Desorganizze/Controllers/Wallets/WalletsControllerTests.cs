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

        [Theory]
        [InlineData(1, HttpStatusCode.OK)]
        [InlineData(0, HttpStatusCode.BadRequest)]
        [InlineData(-1, HttpStatusCode.NotFound)]
        public async Task Should_Get_StatusCode_Based_On_UserId(int userId, HttpStatusCode statusCode)
        {
            var token = await GetTokenAsync();

            var response = await GetAsync($"wallets/{userId}/user", token);

            response.StatusCode.Should().Be(statusCode);
        }

        [Fact]
        public async Task Should_Create_Valid_Account()
        {
            var adminId = 1;
            var token = await GetTokenAsync();
            var getResponse = await GetAsync($"wallets/{adminId}/user", token);
            var wallet = await DeserializeAsync<GetWalletFromUserId>(getResponse);
            var inputModel = new
            {
                Name = "Account Name Test"
            };

            var response = await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModel);
            var newAccountResponseDto = await DeserializeAsync<PostNewAccountDto>(response);

            response.EnsureSuccessStatusCode();
            newAccountResponseDto.Name.Should().Be(inputModel.Name);
            newAccountResponseDto.Id.Should().NotBe(default);
        }
    }
}

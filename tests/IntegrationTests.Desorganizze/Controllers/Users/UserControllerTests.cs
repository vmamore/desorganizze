using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze.Controllers.Users
{
    [Collection("Server collection")]
    public class UserControllerTests : IntegrationTest
    {
        private const string BASE_ENDPOINT = "/api/users";

        public UserControllerTests(ServerFixture serverFixture) : base(serverFixture) {}

        [Fact]
        public async Task Should_Return_OK_When_Valid_Input_For_Creating_User_Is_Sent()
        {
            var inputModel = new
            {
                username = "mariab",
                password = "m@r1ab",
                cpf = "45540096029",
                firstname = "Maria",
                lastname = "Barbosa"
            };

            var response = await PostAsync(BASE_ENDPOINT, inputModel);
            var contentDeserialized = await DeserializeAsync<PostUserResponse>(response);

            response.EnsureSuccessStatusCode();
            contentDeserialized.Username.Should().Be(inputModel.username);
            contentDeserialized.CPF.Should().Be(inputModel.cpf);
            contentDeserialized.FirstName.Should().Be(inputModel.firstname);
            contentDeserialized.LastName.Should().Be(inputModel.lastname);
            contentDeserialized.Password.Should().BeNull();
        }
    }
}

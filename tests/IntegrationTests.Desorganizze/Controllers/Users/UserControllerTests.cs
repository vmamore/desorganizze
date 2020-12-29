using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze.Controllers.Users
{
    [Collection("Server collection")]
    public class UserControllerTests : IntegrationTest
    {
        private const string BASE_ENDPOINT = "/api/users";

        public UserControllerTests(ServerFixture serverFixture) : base(serverFixture) { }

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

        [Fact]
        public async Task Should_Return_All_Users()
        {
            var token = await GetTokenAsync();

            var response = await GetAsync(BASE_ENDPOINT, token);
            var contentDeserialized = await DeserializeListAsync<GetUsersResponse>(response);

            response.EnsureSuccessStatusCode();
            contentDeserialized.Should().ContainSingle();
        }

        [Fact]
        public async Task Should_Return_User_By_Id()
        {
            var token = await GetTokenAsync();
            var id = 1;

            var response = await GetAsync($"{BASE_ENDPOINT}/{id}", token);
            var contentDeserialized = await DeserializeAsync<GetUsersResponse>(response);

            response.EnsureSuccessStatusCode();
            contentDeserialized.Username.Should().Be("vmamore");
        }

        [Fact]
        public async Task Should_Return_Not_Found_When_User_Doesnt_Exist()
        {
            var token = await GetTokenAsync();
            var id = -1;

            var response = await GetAsync($"{BASE_ENDPOINT}/{id}", token);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Return_BadRequest_When_Username_Is_Already_In_Use()
        {
            var inputModel = new
            {
                username = "vmamore",
                password = "m@r1ab",
                cpf = "45540096029",
                firstname = "Maria",
                lastname = "Barbosa"
            };

            var response = await PostAsync(BASE_ENDPOINT, inputModel);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("", "pa$$word", "45540096029", "Maria", "Barbosa")]
        [InlineData("vmamore", "", "45540096029", "Maria", "Barbosa")]
        [InlineData("vmamore", "pa$$word", "", "Maria", "Barbosa")]
        [InlineData("vmamore", "pa$$word", "45540096029", "", "Barbosa")]
        [InlineData("vmamore", "pa$$word", "45540096029", "Maria", "")]
        public async Task Should_Return_BadRequest_When_Input_Model_Is_Invalid(string username, string password, 
            string cpf, string firstName, string lastName)
        {
            var inputModel = new
            {
                username,
                password,
                cpf,
                firstName,
                lastName
            };

            var response = await PostAsync(BASE_ENDPOINT, inputModel);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

using Bogus;
using FluentAssertions;
using FunctionalTests.Desorganizze.Clients;
using FunctionalTests.Desorganizze.Utils;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.Desorganizze.Controllers.Users
{
    [Collection("Server collection")]
    public class UserControllerTests : FunctionalTest
    {
        private readonly UserClient _userClient;

        public UserControllerTests(ServerFixture serverFixture) : base(serverFixture)
        {
            _userClient = new UserClient(serverFixture.Client);
        }

        [Fact]
        public async Task Should_Return_All_Users()
        {
            var response = await _userClient.GetUsersAsync();
            var contentDeserialized = await DeserializeListAsync<GetUsersResponse>(response);

            response.EnsureSuccessStatusCode();
            contentDeserialized.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Return_OK_When_Valid_Input_For_Creating_User_Is_Sent()
        {
            var inputModel = new
            {
                username = new Faker().Person.LastName + "test",
                password = "m@r1ab",
                cpf = "45540096029",
                firstname = "Maria",
                lastname = "Barbosa"
            };

            var response = await _userClient.CreateUserAsync(inputModel);

            response.EnsureSuccessStatusCode();

            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var bodyData = await JsonDocument.ParseAsync(responseBody);
            bodyData.RootElement.GetProperty("username").GetString().Should().Be(inputModel.username);
            bodyData.RootElement.GetProperty("cpf").GetString().Should().Be(inputModel.cpf);
            bodyData.RootElement.GetProperty("firstName").GetString().Should().Be(inputModel.firstname);
            bodyData.RootElement.GetProperty("lastName").GetString().Should().Be(inputModel.lastname);
            bodyData.RootElement.TryGetProperty("password", out _).Should().BeFalse();
        }

        [Fact]
        public async Task Should_Return_User_By_Id()
        {
            var id = 1;
            var response = await _userClient.GetUserByIdAsync(id);
            var contentDeserialized = await DeserializeAsync<GetUsersResponse>(response);

            response.EnsureSuccessStatusCode();
            contentDeserialized.Username.Should().Be("vmamore");
        }

        [Fact]
        public async Task Should_Return_Not_Found_When_User_Doesnt_Exist()
        {
            var id = -1;

            var response = await _userClient.GetUserByIdAsync(id);

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

            var response = await _userClient.CreateUserAsync(inputModel);

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

            var response = await _userClient.CreateUserAsync(inputModel);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

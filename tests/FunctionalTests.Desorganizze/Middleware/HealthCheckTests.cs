using FluentAssertions;
using FunctionalTests.Desorganizze.Utils;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.Desorganizze.Middleware
{
    [Collection("Server collection")]
    public class HealthCheckTests : FunctionalTest
    {
        public HealthCheckTests(ServerFixture serverFixture) : base(serverFixture) { }

        [Fact]
        public async Task Should_Return_200_And_Healthy()
        {
            var response = await GetAsync("/health");

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            responseContent.Should().Be("Healthy");
        }
    }
}

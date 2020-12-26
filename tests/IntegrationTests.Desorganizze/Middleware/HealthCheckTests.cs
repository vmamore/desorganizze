using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze.Middleware
{
    [Collection("Server collection")]
    public class HealthCheckTests
    {
        private ServerFixture _server;
        public HealthCheckTests(ServerFixture serverFixture)
        {
            _server = serverFixture;
        }

        [Fact]
        public async Task Should_Return_200_And_Healthy()
        {
            var response = await _server.Client.GetAsync("/health");

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            responseContent.Should().Be("Healthy");
        }
    }
}

using Desorganizze;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.IO;
using System.Threading.Tasks;
using ThrowawayDb.Postgres;
using Xunit;

namespace IntegrationTests.Desorganizze
{
    public class SampleIntegrationTest
    {
        [Fact]
        public async Task Should_Return_Healthy()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseConfiguration(config);
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                });

            var host = await hostBuilder.StartAsync();

            var client = host.GetTestClient();

            var response = await client.GetAsync("/health");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Healthy", responseString);
        }

        [Fact]
        public async Task Should_Create_Database()
        {
            using (var database = ThrowawayDatabase.Create(connectionString: "Server=localhost;Port=5433;User Id=postgres;Password=postgres;", databaseNamePrefix: "desorganizze_test_"))
            {
                using (var connection = new NpgsqlConnection(database.ConnectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT 1", connection))
                    {
                        var result = Convert.ToInt32(cmd.ExecuteScalar());
                        Assert.Equal(1, result);
                    }
                }
            }
        }
    }
}

using Desorganizze;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Threading.Tasks;
using ThrowawayDb.Postgres;
using Xunit;

namespace IntegrationTests.Desorganizze
{
    public class SampleIntegrationTest
    {
        [Fact]
        public async Task Should_Return_Hello_World()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.Configure(app => app.Run(async ctx => await ctx.Response.WriteAsync("Hello World!")));
                });

            var host = await hostBuilder.StartAsync();

            var client = host.GetTestClient();

            var response = await client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Hello World!", responseString);
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

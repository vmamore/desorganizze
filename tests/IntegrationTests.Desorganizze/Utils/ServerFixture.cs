using Desorganizze;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.Http;

namespace IntegrationTests.Desorganizze.Utils
{
    public class ServerFixture : DatabaseFixture, IDisposable
    {
        public HttpClient Client { get; private set; }

        public ServerFixture()
        {
            CreateWebHost();
        }

        private void CreateWebHost()
        {
            Environment.SetEnvironmentVariable("DATABASE_NAME", Database.Name);
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "IntegrationTest");

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

            var host = hostBuilder.Start();

            Client = host.GetTestClient();
        }
    }
}

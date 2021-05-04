using Desorganizze;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Net.Http;

namespace FunctionalTests.Desorganizze.Utils
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
            //Environment.SetEnvironmentVariable("DATABASE_NAME", Database.Name);
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "QA");

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.QA.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseConfiguration(config);
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>()
                           .UseSerilog();
                });

            var host = hostBuilder.Start();

            Client = host.GetTestClient();
        }
    }
}

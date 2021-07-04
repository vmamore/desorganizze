using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionalTests.Desorganizze.Utils
{
    public abstract class FunctionalTest : IDisposable
    {
        private ServerFixture _server;
        public FunctionalTest(ServerFixture serverFixture)
        {
            _server = serverFixture;
        }

        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        protected async Task<IEnumerable<T>> DeserializeListAsync<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        protected async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _server.Client.GetAsync(endpoint);
        }

        public void Dispose()
        {
            var session = _server.Services.GetRequiredService<ISession>();
            var transactionsDeleteCount = session.CreateSQLQuery("DELETE FROM transactions").ExecuteUpdate();
            var accountsDeleteCount = session.CreateSQLQuery("DELETE FROM accounts").ExecuteUpdate();
        }
    }
}

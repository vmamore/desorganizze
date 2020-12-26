using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntegrationTests.Desorganizze.Utils
{
    public abstract class IntegrationTest
    {
        private ServerFixture _server;
        public IntegrationTest(ServerFixture serverFixture)
        {
            _server = serverFixture;
        }

        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        protected async Task<HttpResponseMessage> PostAsync(string endpoint, object model)
        {
            var bodyRequest = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            return await _server.Client.PostAsync(endpoint, bodyRequest);
        }

        protected async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _server.Client.GetAsync(endpoint);
        }
    }
}

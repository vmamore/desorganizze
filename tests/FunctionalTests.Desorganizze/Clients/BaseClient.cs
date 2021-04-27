using FunctionalTests.Desorganizze.Controllers.Login;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionalTests.Desorganizze.Clients
{
    public abstract class BaseClient
    {
        protected readonly HttpClient Client;

        public BaseClient(HttpClient client) => Client = client;

        public async Task<string> GetTokenAsync()
        {
            string LOGIN_ENDPOINT = "/api/login";
            var adminModel = new
            {
                username = "vmamore",
                password = "teste123"
            };
            var bodyRequest = new StringContent(JsonSerializer.Serialize(adminModel), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(LOGIN_ENDPOINT, bodyRequest);
            var loginResponseDto = await DeserializeAsync<LoginPostResponseDto>(response);
            return loginResponseDto.Token;
        }

        protected async Task<HttpResponseMessage> PostAsync(string endpoint, object model, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var bodyRequest = ParseBody(model);
            return await Client.PostAsync(endpoint, bodyRequest);
        }

        protected async Task<HttpResponseMessage> PutAsync(string endpoint, object model, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var bodyRequest = ParseBody(model);
            return await Client.PutAsync(endpoint, bodyRequest);
        }

        protected async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await Client.GetAsync(endpoint);
        }

        protected async Task<HttpResponseMessage> GetAsync(string endpoint, string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await Client.GetAsync(endpoint);
        }

        private async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
            => await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        private StringContent ParseBody(object model) 
            => new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
    }
}

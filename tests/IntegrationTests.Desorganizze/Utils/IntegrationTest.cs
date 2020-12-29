using IntegrationTests.Desorganizze.Controllers.Login;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        protected async Task<IEnumerable<T>> DeserializeListAsync<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
        protected async Task<HttpResponseMessage> GetAsync(string endpoint, string token)
        {
            _server.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return await _server.Client.GetAsync(endpoint);
        }

        protected async Task<string> GetTokenAsync()
        {
            string LOGIN_ENDPOINT = "/api/login";
            var adminModel = new
            {
                username = "vmamore",
                password = "teste123"
            };
            var bodyRequest = new StringContent(JsonSerializer.Serialize(adminModel), Encoding.UTF8, "application/json");
            var response = await _server.Client.PostAsync(LOGIN_ENDPOINT, bodyRequest);
            var loginResponseDto = await DeserializeAsync<LoginPostResponseDto>(response);
            return loginResponseDto.Token;
        }
    }
}

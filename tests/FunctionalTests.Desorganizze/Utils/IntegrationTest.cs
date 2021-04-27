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

        #region LoginController
        public async Task<HttpResponseMessage> LoginAsync(object body)
            => await PostAsync("/api/login", body);
        #endregion

        #region UserController
        public async Task<HttpResponseMessage> GetUsersAsync()
            => await GetAsync("/api/users", await GetTokenAsync());

        public async Task<HttpResponseMessage> GetUserByIdAsync(int userId)
            => await GetAsync($"/api/users/{userId}", await GetTokenAsync());

        public async Task<HttpResponseMessage> CreateUserAsync(object body)
            => await PostAsync("/api/users", body);
        #endregion

        #region WalletsController
        public async Task<HttpResponseMessage> GetWalletByUserId(int userId)
            => await GetAsync($"wallets/{userId}/user", await GetTokenAsync());

        public async Task<HttpResponseMessage> GetAccountsFromWallet(Guid walletId)
            => await GetAsync($"api/wallets/{walletId}/accounts", await GetTokenAsync());

        public async Task<HttpResponseMessage> GetAllTransactionsFromWallet(Guid walletId)
            => await GetAsync($"api/wallets/{walletId}/transactions");

        public async Task<HttpResponseMessage> CreateNewAccount(Guid walletId, object body)
            => await PostAsync($"api/wallets/{walletId}/accounts", body);

        public async Task<HttpResponseMessage> CreateNewTransaction(Guid walletId, Guid accountId, object body)
            => await PostAsync($"api/wallets/{walletId}/accounts/{accountId}/transaction", body);

        public async Task<HttpResponseMessage> TransferBetweenAccounts(Guid walletId, object body)
            => await PutAsync($"api/wallets/{walletId}/accounts", body);
        #endregion

        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        protected async Task<IEnumerable<T>> DeserializeListAsync<T>(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        protected async Task<HttpResponseMessage> PostAsync(string endpoint, object model, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
                _server.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var bodyRequest = ParseBody(model);
            return await _server.Client.PostAsync(endpoint, bodyRequest);
        }

        protected async Task<HttpResponseMessage> PutAsync(string endpoint, object model, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
                _server.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var bodyRequest = ParseBody(model);
            return await _server.Client.PutAsync(endpoint, bodyRequest);
        }

        protected async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _server.Client.GetAsync(endpoint);
        }

        protected async Task<HttpResponseMessage> GetAsync(string endpoint, string token)
        {
            _server.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        private StringContent ParseBody(object model) => new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
    }
}

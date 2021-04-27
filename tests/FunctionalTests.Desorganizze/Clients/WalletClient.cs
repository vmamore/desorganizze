using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FunctionalTests.Desorganizze.Clients
{
    public class WalletClient : BaseClient
    {
        public WalletClient(HttpClient client) : base(client) {}

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
    }
}

﻿using Desorganizze.Domain;
using FluentAssertions;
using IntegrationTests.Desorganizze.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Desorganizze.Controllers.Wallets
{
    [Collection("Server collection")]
    public class WalletsControllerTests : IntegrationTest
    {
        public WalletsControllerTests(ServerFixture serverFixture) : base(serverFixture) {}

        [Theory]
        [InlineData(1, HttpStatusCode.OK)]
        [InlineData(0, HttpStatusCode.BadRequest)]
        [InlineData(-1, HttpStatusCode.NotFound)]
        public async Task Should_Get_StatusCode_Based_On_UserId(int userId, HttpStatusCode statusCode)
        {
            var token = await GetTokenAsync();

            var response = await GetAsync($"wallets/{userId}/user", token);

            response.StatusCode.Should().Be(statusCode);
        }

        [Fact]
        public async Task Should_Create_Valid_Account()
        {
            var adminId = 1;
            var token = await GetTokenAsync();
            var getResponse = await GetAsync($"wallets/{adminId}/user", token);
            var wallet = await DeserializeAsync<GetWalletFromUserId>(getResponse);
            var inputModel = new
            {
                Name = "Account Name Test"
            };

            var response = await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModel);
            var teste = await response.Content.ReadAsStringAsync();
            var newAccountResponseDto = await DeserializeAsync<PostNewAccountDto>(response);

            response.EnsureSuccessStatusCode();
            newAccountResponseDto.Name.Should().Be(inputModel.Name);
            newAccountResponseDto.Id.Should().NotBe(default);
        }

        [Fact]
        public async Task Should_Create_Transaction_To_Valid_Accounts()
        {
            var adminId = 1;
            var token = await GetTokenAsync();
            var getResponse = await GetAsync($"wallets/{adminId}/user", token);
            var wallet = await DeserializeAsync<GetWalletFromUserId>(getResponse);
            var inputModel = new
            {
                Name = "Account Name Test"
            };
            var createdAccountResponse = await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModel);
            createdAccountResponse.EnsureSuccessStatusCode();
            var newAccountResponseDto = await DeserializeAsync<PostNewAccountDto>(createdAccountResponse);
            var inputTransactionModel = new
            {
                Amount = 1000.0m,
                Type = (int) TransactionType.Add
            };

            var createdTransactionResponse = await PostAsync($"api/wallets/{wallet.WalletId}/accounts/{newAccountResponseDto.Id}", inputTransactionModel);

            var createdTransactionResponseString = await createdTransactionResponse.Content.ReadAsStringAsync();
            createdTransactionResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_Transfer_Money_Between_Valid_Accounts()
        {
            var adminId = 1;
            var token = await GetTokenAsync();
            var getResponse = await GetAsync($"wallets/{adminId}/user", token);
            var wallet = await DeserializeAsync<GetWalletFromUserId>(getResponse);
            var inputModelAccountOne = new
            {
                Name = "Account Name Test One"
            };
            var responseFromAccountOne = await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModelAccountOne, token);
            var newAccountResponseOneDto = await DeserializeAsync<PostNewAccountDto>(responseFromAccountOne);
            var inputModelAccountTwo = new
            {
                Name = "Account Name Test Two"
            };
            var responseFromAccountTwo = await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModelAccountTwo, token);
            var newAccountResponseTwoDto = await DeserializeAsync<PostNewAccountDto>(responseFromAccountTwo);
            var inputTransactionModel = new
            {
                Amount = 1000.0m,
                Type = (int)TransactionType.Add
            };
            await PostAsync($"api/wallets/{wallet.WalletId}/accounts/{newAccountResponseOneDto.Id}", inputTransactionModel, token);
            var inputModel = new
            {
                amount = 1000.0m,
                sourceAccountId = newAccountResponseOneDto.Id,
                recipientAccountId = newAccountResponseTwoDto.Id
            };

            var result = await PutAsync($"api/wallets/{wallet.WalletId}/accounts", inputModel);

            result.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_Get_All_Accounts_From_User_Wallet()
        {
            var adminId = 1;
            var token = await GetTokenAsync();
            var getResponse = await GetAsync($"wallets/{adminId}/user", token);
            var wallet = await DeserializeAsync<GetWalletFromUserId>(getResponse);
            var inputModelAccountOne = new
            {
                Name = "Account Name Test One"
            };
            await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModelAccountOne);
            var inputModelAccountTwo = new
            {
                Name = "Account Name Test Two"
            };
             await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModelAccountTwo);

            var response = await GetAsync($"api/wallets/{wallet.WalletId}/accounts", token);
            var responseContent = await DeserializeListAsync<GetAccountsFromWalletId>(response);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_Get_All_Transactions_From_Wallet()
        {
            var adminId = 1;
            var token = await GetTokenAsync();
            var getResponse = await GetAsync($"wallets/{adminId}/user", token);
            var wallet = await DeserializeAsync<GetWalletFromUserId>(getResponse);
            var inputModelAccount = new
            {
                Name = "Account Name Test One"
            };
            var responseFromAccount = await PostAsync($"api/wallets/{wallet.WalletId}/accounts", inputModelAccount);
            var newAccountResponseDto = await DeserializeAsync<PostNewAccountDto>(responseFromAccount);

            var inputTransactionModel = new
            {
                Amount = 1000.0m,
                Type = (int)TransactionType.Add
            };

            await PostAsync($"api/wallets/{wallet.WalletId}/accounts/{newAccountResponseDto.Id}", inputTransactionModel);
            await PostAsync($"api/wallets/{wallet.WalletId}/accounts/{newAccountResponseDto.Id}", inputTransactionModel);
            await PostAsync($"api/wallets/{wallet.WalletId}/accounts/{newAccountResponseDto.Id}", inputTransactionModel);

            var response = await GetAsync($"api/wallets/{wallet.WalletId}/transactions", token);
            var responseContent = await DeserializeListAsync<GetTransactionsFromWallet>(response);

            response.EnsureSuccessStatusCode();
            responseContent.Should().Contain(c => c.AccountName == inputModelAccount.Name);
        }
    }
}
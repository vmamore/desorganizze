using Desorganizze.Domain.Repositories;
using Desorganizze.Infra.CQRS.Commands;
using System;
using System.Threading.Tasks;

namespace Desorganizze.Application.Commands.Wallets.Handlers
{
    public class TransferBetweenAccountsHandler : ICommandHandler<TransferBetweenAccounts>
    {
        private readonly IWalletRepository _walletRepository;

        public TransferBetweenAccountsHandler(IWalletRepository walletRepository) => _walletRepository = walletRepository;

        public async Task<Result> ExecuteAsync(TransferBetweenAccounts command)
        {
            try
            {
                var wallet = await _walletRepository.GetWalletById(command.WalletId);

                if (wallet == null) return Result.Fail($"Wallet not found.");

                wallet.TransferBetweenAccounts(command.RecipientAccountId, command.SourceAccountId, command.Amount);

                await _walletRepository.SaveOrUpdateAsync(wallet);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}

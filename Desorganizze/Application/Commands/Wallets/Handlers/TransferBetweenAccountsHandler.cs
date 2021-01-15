using Desorganizze.Domain.Repositories;
using Desorganizze.Infra.CQRS.Commands;
using NHibernate;
using System.Threading.Tasks;

namespace Desorganizze.Application.Commands.Wallets.Handlers
{
    public class TransferBetweenAccountsHandler : ICommandHandler<TransferBetweenAccounts>
    {
        private readonly ISession _session;
        private readonly IWalletRepository _walletRepository;

        public TransferBetweenAccountsHandler(IWalletRepository walletRepository,
            ISession session)
        {
            _walletRepository = walletRepository;
            _session = session;
        }
        public async Task<Result> ExecuteAsync(TransferBetweenAccounts command)
        {
            var wallet = await _walletRepository.GetWalletById(command.WalletId);

            if (wallet == null) return Result.Fail($"Wallet not found.");

            wallet.TransferBetweenAccounts(command.RecipientAccountId, command.SourceAccountId, command.Amount);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(wallet);
            await transaction.CommitAsync();

            return Result.Ok();
        }
    }
}

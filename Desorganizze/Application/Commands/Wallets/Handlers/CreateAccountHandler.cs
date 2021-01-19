using Desorganizze.Domain.Repositories;
using Desorganizze.Infra.CQRS.Commands;
using NHibernate;
using System.Threading.Tasks;

namespace Desorganizze.Application.Commands.Wallets.Handlers
{
    public class CreateAccountHandler : ICommandHandler<CreateAccount>
    {
        private readonly ISession _session;
        private readonly IWalletRepository _walletRepository;

        public CreateAccountHandler(IWalletRepository walletRepository,
            ISession session)
        {
            _walletRepository = walletRepository;
            _session = session;
        }

        public async Task<Result> ExecuteAsync(CreateAccount command)
        {
            var wallet = await _walletRepository.GetWalletById(command.WalletId);

            if (wallet == null) return Result.Fail($"Wallet not found.");

            var newAccount = wallet.NewAccount(command.Name);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(newAccount);
            await transaction.CommitAsync();

            return Result.Ok(new
            {
               Id = newAccount.Id,
               Name = newAccount.Name.Valor
            });
        }
    }
}

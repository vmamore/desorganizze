namespace Desorganizze.Application.Commands.Wallets.Handlers
{
    using Desorganizze.Domain.Repositories;
    using Desorganizze.Infra.CQRS.Commands;
    using NHibernate;
    using System.Threading.Tasks;

    public class CreateCategoryHandler : ICommandHandler<CreateCategory>
    {
        private readonly ISession _session;
        private readonly IWalletRepository _walletRepository;

        public CreateCategoryHandler(IWalletRepository walletRepository,
            ISession session)
        {
            _walletRepository = walletRepository;
            _session = session;
        }

        public async Task<Result> ExecuteAsync(CreateCategory command)
        {
            var wallet = await _walletRepository.GetWalletById(command.WalletId);

            if (wallet == null) return Result.Fail($"Wallet not found.");

            wallet.CreateNewCategory(command.Description);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(wallet);
            await transaction.CommitAsync();

            return Result.Ok();
        }
    }
}

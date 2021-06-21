using Desorganizze.Domain;
using Desorganizze.Infra.CQRS.Commands;
using NHibernate;
using NHibernate.Linq;
using System.Threading.Tasks;

namespace Desorganizze.Application.Commands.Wallets.Handlers
{
    public class CreateTransactionHandler : ICommandHandler<CreateTransaction>
    {
        private readonly ISession _session;
        public CreateTransactionHandler(ISession session)
        {
            _session = session;
        }

        public async Task<Result> ExecuteAsync(CreateTransaction command)
        {
            var accountPersited = await _session.Query<Account>()
                .FirstAsync(a => a.Id == command.AccountId);

            if (accountPersited == null) return Result.Fail($"Account not found.");

            var category = await _session.Query<Category>()
                .FirstAsync(c => c.Id == command.CategoryId);

            var createdTransaction = accountPersited.NewTransaction(command.Amount, command.Type, category);

            using var transaction = _session.BeginTransaction();
            await _session.SaveOrUpdateAsync(createdTransaction);
            await transaction.CommitAsync();

            return Result.Ok();
        }
    }
}

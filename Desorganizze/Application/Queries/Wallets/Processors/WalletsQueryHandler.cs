using Desorganizze.Application.Queries.Wallets.Parameters;
using Desorganizze.Application.Queries.Wallets.ReadModel;
using Desorganizze.Domain;
using Desorganizze.Infra.CQRS.Queries;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desorganizze.Application.Queries.Wallets.Processors
{
    public class WalletsQueryHandler :
        IQueryHandler<GetAllAccountsFromUser, IEnumerable<AccountFromWalletDto>>,
        IQueryHandler<GetTransactionsFromUser, IEnumerable<TransactionQueryDto>>,
        IQueryHandler<GetWalletWithAccountsFromUser, WalletWithAcountDto>
    {
        private readonly ISession _session;
        public WalletsQueryHandler(ISession session) => _session = session;

        public async Task<IEnumerable<AccountFromWalletDto>> ExecuteQueryAsync(GetAllAccountsFromUser queryParameter) =>
            await _session.Query<Account>()
                .Where(acc => acc.Wallet.Id == queryParameter.WalletId)
                .Select(x => new AccountFromWalletDto(x.Id, x.GetBalance.Amount, x.Name.Valor))
                .ToListAsync();

        public async Task<IEnumerable<TransactionQueryDto>> ExecuteQueryAsync(GetTransactionsFromUser queryParameter) =>
            await _session.Query<Transaction>()
                .Where(t => t.Account.Wallet.Id == queryParameter.WalletId)
                .Select(x => new TransactionQueryDto(x.TotalAmount.Amount, x.Type, x.CreatedDate, x.Account.Name.Valor))
                .ToListAsync();

        public async Task<WalletWithAcountDto> ExecuteQueryAsync(GetWalletWithAccountsFromUser queryParameter) =>
            await _session.Query<Wallet>()
                .Where(wallet => wallet.User.Id == queryParameter.UserId)
                .Select(wallet => new WalletWithAcountDto
                {
                    WalletId = wallet.Id,
                    Accounts = wallet.Accounts.Select(account => new AccountDto { AccountId = account.Id, Namme = account.Name.Valor })
                })
                .FirstOrDefaultAsync();
    }
}

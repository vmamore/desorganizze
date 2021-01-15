using Desorganizze.Domain;
using Desorganizze.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Threading.Tasks;

namespace Desorganizze.Infra.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ISession _session;
        public WalletRepository(ISession session) => _session = session;

        public async Task<Wallet> GetWalletById(Guid walletId) => await _session.Query<Wallet>().FirstOrDefaultAsync(w => w.Id == walletId);

        public async Task<Wallet> GetWalletByUserId(int userId) => await _session.Query<Wallet>().FirstOrDefaultAsync(w => w.User.Id == userId);
    }
}

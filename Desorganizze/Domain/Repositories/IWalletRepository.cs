using System;
using System.Threading.Tasks;

namespace Desorganizze.Domain.Repositories
{
    public interface IWalletRepository
    {
        Task SaveOrUpdateAsync(Wallet wallet);
        Task<Wallet> GetWalletByUserId(int userId);

        Task<Wallet> GetWalletById(Guid walletId);
    }
}

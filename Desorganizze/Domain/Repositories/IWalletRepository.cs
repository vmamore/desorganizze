using System.Threading.Tasks;

namespace Desorganizze.Domain.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> GetWalletByUserId(int userId);
    }
}

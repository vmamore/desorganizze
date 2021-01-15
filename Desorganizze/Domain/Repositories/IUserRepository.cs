using System.Threading.Tasks;

namespace Desorganizze.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task SaveAsync(User user);
    }
}

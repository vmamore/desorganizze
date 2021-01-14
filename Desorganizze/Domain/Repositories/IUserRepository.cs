using System.Threading.Tasks;

namespace Desorganizze.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByNameAsync(string name);

        Task SaveAsync(User user);
    }
}

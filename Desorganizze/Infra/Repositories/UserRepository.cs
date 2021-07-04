using Desorganizze.Domain;
using Desorganizze.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;
using System.Threading.Tasks;

namespace Desorganizze.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;
        public UserRepository(ISession session) => _session = session;

        public async Task<User> GetUserByIdAsync(int userId) => await _session.Query<User>().FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<User> GetUserByNameAsync(string name) => await _session.Query<User>().FirstOrDefaultAsync(u => u.Username.Value == name);

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password) => await _session.Query<User>().FirstOrDefaultAsync(u => u.Username.Value == username &&
                                                                                                                                                             u.Password.Value == password);

        public async Task SaveAsync(User user) => await _session.SaveAsync(user);
    }
}

using Desorganizze.Application.Queries.Users.Parameters;
using Desorganizze.Application.Queries.Users.ReadModel;
using Desorganizze.Domain;
using Desorganizze.Infra.CQRS.Queries;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desorganizze.Application.Queries.Users.Processors
{
    public class UserQueryHandler :
        IQueryHandler<GetUserById, UserDtoItem>,
        IQueryHandler<GetAllUsers, IEnumerable<UserDtoItem>>
    {
        private readonly ISession _session;

        public UserQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<UserDtoItem> ExecuteQueryAsync(GetUserById queryParameter) =>
        await _session.Query<User>()
                .Where(x => x.Id == queryParameter.Id)
                .Select(x => new UserDtoItem
                {
                    Username = x.Username.Valor
                })
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<UserDtoItem>> ExecuteQueryAsync(GetAllUsers queryParameter) =>
            await _session
                .Query<User>()
                .Select(x => new UserDtoItem { Id = x.Id, Username = x.Username.Valor })
                .ToListAsync();
    }
}

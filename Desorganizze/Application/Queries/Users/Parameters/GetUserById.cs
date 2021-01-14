using Desorganizze.Infra.CQRS.Queries;

namespace Desorganizze.Application.Queries.Users.Parameters
{
    public class GetUserById : IQuery
    {
        public GetUserById(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

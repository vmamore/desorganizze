using System.Threading.Tasks;

namespace Desorganizze.Infra.CQRS.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> ExecuteQueryAsync<TQueryParameters, TResult>(TQueryParameters queryParameters)
            where TQueryParameters : IQuery;
    }
}

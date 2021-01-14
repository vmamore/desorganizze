using System.Threading.Tasks;

namespace Desorganizze.Infra.CQRS.Queries
{
    public interface IQueryHandler<in TQueryParameter, TResult>
        where TQueryParameter : IQuery
    {
        Task<TResult> ExecuteQueryAsync(TQueryParameter queryParameter);
    }
}

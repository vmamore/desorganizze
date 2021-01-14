using System;
using System.Threading.Tasks;

namespace Desorganizze.Infra.CQRS.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly DependencyResolver _dependencyResolver;

        public QueryProcessor(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task<TResult> ExecuteQueryAsync<TQueryParameters, TResult>(TQueryParameters queryParameters)
            where TQueryParameters : IQuery
        {
            if (queryParameters == null)
                throw new ArgumentNullException(nameof(queryParameters));

            var queryHandler = _dependencyResolver.Resolve<IQueryHandler<TQueryParameters, TResult>>();

            return await queryHandler.ExecuteQueryAsync(queryParameters);
        }
    }
}

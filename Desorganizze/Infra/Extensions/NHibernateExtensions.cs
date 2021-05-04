using Desorganizze.Infra.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;

namespace Desorganizze.Infra.Extensions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            System.Console.WriteLine(connectionString);

            var sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard
                    .ConnectionString(connectionString)
                    .Dialect<PostgreSQL82Dialect>()
                    .ShowSql())
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<UserMap>())
                .BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddTransient(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;

namespace Desorganizze.Infra.Utils
{
    public static class ConnectionStringBuilder
    {
        public static string Create(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:PgSql"];

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") return connectionString;

            return connectionString.Replace("{DATABASE_NAME}", Environment.GetEnvironmentVariable("DATABASE_NAME"));
        }
    }
}

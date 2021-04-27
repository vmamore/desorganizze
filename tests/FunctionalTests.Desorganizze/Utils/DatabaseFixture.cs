using System;
using ThrowawayDb.Postgres;

namespace FunctionalTests.Desorganizze.Utils
{
    public class DatabaseFixture : IDisposable
    {
        protected ThrowawayDatabase Database { get; private set; }

        public DatabaseFixture()
        {
            Database = ThrowawayDatabase.Create(connectionString: "Server=localhost;Port=5433;User Id=postgres;Password=postgres;", databaseNamePrefix: "desorganizze_test_");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
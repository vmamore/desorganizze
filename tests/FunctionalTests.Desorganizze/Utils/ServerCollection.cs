using Xunit;

namespace IntegrationTests.Desorganizze.Utils
{
    [CollectionDefinition("Server collection")]
    public class ServerCollection : ICollectionFixture<ServerFixture>
    {
    }
}

using Xunit;

namespace FunctionalTests.Desorganizze.Utils
{
    [CollectionDefinition("Server collection")]
    public class ServerCollection : ICollectionFixture<ServerFixture>
    {
    }
}

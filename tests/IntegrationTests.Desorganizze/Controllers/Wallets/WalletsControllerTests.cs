using IntegrationTests.Desorganizze.Utils;
using Xunit;

namespace IntegrationTests.Desorganizze.Controllers.Wallets
{
    [Collection("Server collection")]
    public class WalletsControllerTests : IntegrationTest
    {
        public WalletsControllerTests(ServerFixture serverFixture) : base(serverFixture) {}
    }
}

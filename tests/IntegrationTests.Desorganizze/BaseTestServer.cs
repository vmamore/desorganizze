using Microsoft.AspNetCore.TestHost;

namespace IntegrationTests.Desorganizze
{
    public class BaseTestServer
    {
        public TestServer Server { get; protected set; }

        public BaseTestServer()
        {
            
        }
    }
}

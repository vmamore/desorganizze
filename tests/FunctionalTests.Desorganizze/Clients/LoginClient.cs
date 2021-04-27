using System.Net.Http;
using System.Threading.Tasks;

namespace FunctionalTests.Desorganizze.Clients
{
    public class LoginClient : BaseClient
    {
        public LoginClient(HttpClient client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> LoginAsync(object body)
            => await PostAsync("/api/login", body);
    }
}

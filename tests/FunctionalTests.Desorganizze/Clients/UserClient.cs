using System.Net.Http;
using System.Threading.Tasks;

namespace FunctionalTests.Desorganizze.Clients
{
    public class UserClient : BaseClient
    {
        public UserClient(HttpClient client) : base(client) {}

        public async Task<HttpResponseMessage> GetUsersAsync()
            => await GetAsync("/api/users", await GetTokenAsync());

        public async Task<HttpResponseMessage> GetUserByIdAsync(int userId)
            => await GetAsync($"/api/users/{userId}", await GetTokenAsync());

        public async Task<HttpResponseMessage> CreateUserAsync(object body)
            => await PostAsync("/api/users", body);
    }
}

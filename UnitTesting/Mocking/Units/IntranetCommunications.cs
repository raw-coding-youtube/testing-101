using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mocking.Units
{
    public class IntranetCommunications
    {
        private readonly HttpClient _client;

        public IntranetCommunications(HttpClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<string>> FetchNames()
        {
            return _client.GetFromJsonAsync<IEnumerable<string>>("/api/names");
        }
    }
}
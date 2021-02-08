using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mocking.Units;
using Xunit;

namespace Mocking
{
    public class IntranetCommunicationsTests
    {
        public class MockHttpHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken
            )
            {
                return Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent("[\"foo\", \"bar\", \"baz\"]"),
                });
            }
        }

        [Fact]
        public async Task FetchNamesFetchesNames()
        {
            var client = new HttpClient(new MockHttpHandler())
            {
                BaseAddress = new("http://example.com")
            };
            var iComm = new IntranetCommunications(client);

            var names = (await iComm.FetchNames()).ToList();

            Assert.Equal(3, names.Count);
            Assert.Contains("foo", names);
            Assert.Contains("bar", names);
            Assert.Contains("baz", names);
        }
    }
}
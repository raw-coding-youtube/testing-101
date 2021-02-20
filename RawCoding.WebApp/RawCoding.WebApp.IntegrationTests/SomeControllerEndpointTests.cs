using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RawCoding.WebApp.IntegrationTests
{
    public class SomeControllerEndpointTests : IClassFixture<AppInstance>
    {
        private readonly AppInstance _instance;

        public SomeControllerEndpointTests(AppInstance instance)
        {
            _instance = instance;
        }

        [Fact]
        public async Task PublicTest()
        {
            var client = _instance.CreateClient();
            var result = await client.GetAsync("/public");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Public", content);
        }

        [Fact]
        public async Task Secure()
        {
            var client = _instance
                .AuthenticatedInstance()
                .CreateClient(new()
                {
                    AllowAutoRedirect = false,
                });

            var result = await client.GetAsync("/secure");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Secure", content);
        }

        [Fact]
        public async Task PrivilegedGod()
        {
            var client = _instance
                .AuthenticatedInstance(new Claim("CustomRoleType", "God"))
                .CreateClient(new()
                {
                    AllowAutoRedirect = false,
                });

            var result = await client.GetAsync("/privileged");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Privileged", content);
        }

        [Fact]
        public async Task PrivilegedAngel()
        {
            var client = _instance
                .AuthenticatedInstance(new Claim("CustomRoleType", "Angel"))
                .CreateClient(new()
                {
                    AllowAutoRedirect = false,
                });

            var result = await client.GetAsync("/privileged");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Privileged", content);
        }

        [Fact]
        public async Task AdminGod()
        {
            var client = _instance
                .AuthenticatedInstance(new Claim("CustomRoleType", "God"))
                .CreateClient(new()
                {
                    AllowAutoRedirect = false,
                });

            var result = await client.GetAsync("/admin");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync();
            Assert.Equal("Admin", content);
        }
    }
}
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Controllers;
using SampleApp.Services;
using Xunit;

namespace SampleApp.IntegrationTesting
{
    public class FileEndpointTests : IClassFixture<FileTestingFixture>
    {
        private readonly FileTestingFixture _factory;

        public FileEndpointTests(FileTestingFixture factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SavesFileToDisk()
        {
            var client = _factory.CreateClient();

            MultipartFormDataContent form = new();

            form.Add(new StreamContent(_factory.TestFile), "file", "base.png");
            var response = await client.PostAsync("/api/files", form);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var fileResponse = await client.GetAsync("/test_images/base.png");
            Assert.Equal(HttpStatusCode.OK, fileResponse.StatusCode);
        }
    }
}
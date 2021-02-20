using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Controllers;
using Xunit;

namespace SampleApp.IntegrationTesting
{
    public class FileTestingFixture : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        public Stream TestFile { get; private set; }
        private string _cleanupPath;

        public async Task InitializeAsync()
        {
            TestFile = await GetTestImage();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                services.Configure<FileSettings>(fs =>
                {
                    fs.Path = "test_images";
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                _cleanupPath = Path.Combine(env.WebRootPath, "test_images");
            });
        }

        private async Task<Stream> GetTestImage()
        {
            var memoryStream = new MemoryStream();
            var fileStream = File.OpenRead("base.png");
            await fileStream.CopyToAsync(memoryStream);
            fileStream.Close();
            return memoryStream;
        }

        public Task DisposeAsync()
        {
            var directoryInfo = new DirectoryInfo(_cleanupPath);
            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            Directory.Delete(_cleanupPath);

            return Task.CompletedTask;
        }
    }
}
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Models;
using SampleApp.Services;
using Xunit;

namespace SampleApp.IntegrationTesting
{
    public class AnimalEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AnimalEndpointTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IAnimalService, AnimalServiceMock>();
                });
            });
        }

        public class AnimalServiceMock : IAnimalService
        {
            public Animal GetAnimal()
            {
                return new()
                {
                    Id = 2,
                    Name = "Foo2",
                    Type = "Bar2",
                };
            }
        }

        [Fact]
        public async Task GetsAnimal()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/animals");
            var animal = await response.Content.ReadFromJsonAsync<Animal>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(animal);
            Assert.Equal(2, animal.Id);
        }
    }
}
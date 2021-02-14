using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting
{
    [Collection(nameof(AnimalCollection))]
    public class AnimalDatabaseQueryTests
    {
        private readonly AnimalSetupFixture _animalSetupFixture;

        public AnimalDatabaseQueryTests(AnimalSetupFixture animalSetupFixture)
        {
            _animalSetupFixture = animalSetupFixture;
        }

        [Fact]
        public async Task AnimalStore_ListsAnimalsFromDatabase()
        {
            var animals = await _animalSetupFixture.Store.GetAnimals();

            Assert.Equal(3, animals.Count);
            Assert.Contains(animals, x => x.Name.Equals("Foo"));
            Assert.Contains(animals, x => x.Name.Equals("Bar"));
            Assert.Contains(animals, x => x.Name.Equals("Baz"));
        }
    }
}
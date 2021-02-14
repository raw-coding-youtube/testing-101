using System;
using System.Threading.Tasks;
using IntegrationTesting.Components.Database;
using Npgsql;
using Xunit;

namespace IntegrationTesting
{
    [Collection(nameof(AnimalCollection))]
    public class AnimalDatabaseTests //: IClassFixture<AnimalSetupFixture>
    {
        private readonly AnimalSetupFixture _animalSetupFixture;

        public AnimalDatabaseTests(AnimalSetupFixture animalSetupFixture)
        {
            _animalSetupFixture = animalSetupFixture;
        }

        [Fact]
        public async Task AnimalStore_SavesAnimalToDatabase()
        {
            var name = Guid.NewGuid().ToString();
            await _animalSetupFixture.Store.SaveAnimal(new(0, name, "Bar"));

            var animals = await _animalSetupFixture.Store.GetAnimals();

            Assert.Single(animals, x => x.Name.Equals(name));
        }

        [Fact]
        public async Task AnimalStore_GetsSavedAnimalByIdFromDatabase()
        {
            var animal = await _animalSetupFixture.Store.GetAnimal(1);

            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }
    }
}
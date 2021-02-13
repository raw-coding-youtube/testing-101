using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTesting.Components.Database
{
    public class AnimalStore : IAnimalStore
    {
        private readonly IDatabase _database;

        public AnimalStore(IDatabase database)
        {
            _database = database;
        }

        public async Task<IList<Animal>> GetAnimals()
        {
            var results = new List<Animal>();
            await using var queryReader = await _database.Query("select * from animals");
            while (await queryReader.ReadAsync())
            {
                results.Add(new(
                    queryReader.GetInt32(0),
                    queryReader.GetString(1),
                    queryReader.GetString(2)
                ));
            }

            return results;
        }

        public async Task<Animal> GetAnimal(int id)
        {
            var query = "select * from animals where id = (@id) limit 1";
            await using var queryReader = await _database.Query(query, new() {["id"] = id.ToString()});
            if (await queryReader.ReadAsync())
            {
                return new(
                    queryReader.GetInt32(0),
                    queryReader.GetString(1),
                    queryReader.GetString(2)
                );
            }

            return null;
        }

        public Task SaveAnimal(Animal animal)
        {
            return _database.Insert(
                "insert into animals (name, type) values (@name, @type)",
                new() {["name"] = animal.Name, ["type"] = animal.Type}
            );
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTesting.Components.Database
{
    public interface IAnimalStore
    {
        Task<IList<Animal>> GetAnimals();
        Task<Animal> GetAnimal(int id);
        Task SaveAnimal(Animal animal);
    }
}
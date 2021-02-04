namespace Mocking.Units
{
    public class CreateSomething
    {
        private readonly IStore _store;

        public CreateSomething(IStore store)
        {
            _store = store;
        }

        public CreateSomethingResult Create(Something something)
        {
            if (something is {Name: {Length: >0}})
            {
                _store.Save(something);
                return new(true);
            }

            return new(false, "Somethings not valid.");
        }

        public record CreateSomethingResult(bool Success, string Error = "");

        public interface IStore
        {
            void Save(Something something);
        }

        public class Something
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
using Mocking.Units;
using Moq;
using Xunit;

namespace Mocking
{
    public class CreateSomethingTests
    {
        // public class StoreMock : CreateSomething.IStore
        // {
        //     public int SaveAttempts { get; set; }
        //     public bool SaveResult { get; set; }
        //     public CreateSomething.Something LastSavedSomething { get; set; }
        //
        //     public bool Save(CreateSomething.Something something)
        //     {
        //         SaveAttempts++;
        //         LastSavedSomething = something;
        //         return SaveResult;
        //     }
        // }

        public readonly Mock<CreateSomething.IStore> _storeMock = new();

        [Fact]
        public void DoesntSaveToDatabaseWhenInvalidSomething()
        {
            CreateSomething createSomething = new(_storeMock.Object);

            var createSomethingResult = createSomething.Create(null);

            Assert.False(createSomethingResult.Success);
            _storeMock.Verify(x => x.Save(It.IsAny<CreateSomething.Something>()), Times.Never);
        }

        [Fact]
        public void SavesSomethingToDatabaseWhenValid()
        {
            var something = new CreateSomething.Something {Name = "Foo"};
            CreateSomething createSomething = new(_storeMock.Object);
            _storeMock.Setup(x => x.Save(something)).Returns(true);

            var createSomethingResult = createSomething.Create(something);

            Assert.True(createSomethingResult.Success);
            _storeMock.Verify(x => x.Save(something), Times.Once);
        }
    }
}
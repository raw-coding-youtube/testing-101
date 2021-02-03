using System.Linq;
using Basics.Units;
using Xunit;

namespace Basics
{
    public class TodoListTests
    {
        [Fact]
        public void Add_SavesTodoItem()
        {
            // arrange
            var list = new TodoList();

            // act
            list.Add(new("Test Content"));

            // assert
            var savedItem = Assert.Single(list.All);
            Assert.NotNull(savedItem);
            Assert.Equal(1, savedItem.Id);
            Assert.Equal("Test Content", savedItem.Content);
            Assert.False(savedItem.Complete);
        }

        [Fact]
        public void TodoItemIdIncrementsEverTimeWeAdd()
        {
            // arrange
            var list = new TodoList();

            // act
            list.Add(new("Test 1"));
            list.Add(new("Test 2"));

            // assert
            var items = list.All.ToArray();
            Assert.Equal(1, items[0].Id);
            Assert.Equal(2, items[1].Id);
        }

        [Fact]
        public void Complete_SetsTodoItemCompleteFlagToTrue()
        {
            // arrange
            var list = new TodoList();
            list.Add(new("Test 1"));

            // act
            list.Complete(1);

            // assert
            var completedItem = Assert.Single(list.All);
            Assert.NotNull(completedItem);
            Assert.Equal(1, completedItem.Id);
            Assert.True(completedItem.Complete);
        }
    }
}
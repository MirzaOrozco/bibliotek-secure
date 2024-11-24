using Xunit;
using Application.Commands.Books;
using Infrastructure.Data;

namespace Tests.Application.Commands.Books
{
    public class CreateBookCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldCreateBookSuccessfully()
        {
            // Arrange
            var db = new FakeDatabase(); // Using FakeDatabase
            var handler = new CreateBookCommandHandler(db);
            var command = new CreateBookCommand
            {
                Title = "New Book",
                AuthorId = 1
            };

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.True(result);
            Assert.Contains(db.Books, b => b.Title == "New Book" && b.AuthorId == 1);
        }
    }
}
using Xunit;
using Domain;
using Infrastructure.Data;
using Application.Commands.Books;
using Application.Handler;

namespace Tests.Application.Commands
{
    public class DeleteBookCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldDeleteBookIfUserIsLibrarian()
        {
            // Arrange
            var db = new FakeDatabase();
            db.Users.Add(new User { Id = 1, Name = "Librarian" }); // Add authorized personal
            var handler = new DeleteBookCommandHandler(db);
            var command = new DeleteBookCommand
            {
                BookId = 1,
                UserId = 1
            };

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.True(result); // Verificate that the delete was succesfully
            Assert.DoesNotContain(db.Books, b => b.Id == 1); // Verificate that the book was deleted
        }

        [Fact]
        public void Handle_ShouldThrowUnauthorizedAccessException_IfUserIsNotLibrarian()
        {
            // Arrange
            var db = new FakeDatabase();
            db.Users.Add(new User { Id = 2, Name = "RegularUser" }); // Add normal user
            var handler = new DeleteBookCommandHandler(db);
            var command = new DeleteBookCommand
            {
                BookId = 1,
                UserId = 2
            };

            // Act & Assert
            Assert.Throws<UnauthorizedAccessException>(() => handler.Handle(command));
        }
    }
}

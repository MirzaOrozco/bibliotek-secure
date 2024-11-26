using Xunit;
using API.Commands.Authors;
using Domain;
using Infrastructure.Data;
using API.Handler;

namespace Library.Tests.API.Commands.Authors
{
    public class UpdateAuthorCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldUpdateAuthorSuccessfully()
        {
            // Arrange
            var db = new FakeDatabase();
            db.Authors.Add(new Author { Id = 1, Name = "George Orwell" }); // Author exist
            var handler = new UpdateAuthorCommandHandler(db);
            var command = new UpdateAuthorCommand
            {
                Id = 1,
                NewName = "Eric Arthur Blair"
            };

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.True(result); 
            Assert.Contains(db.Authors, a => a.Name == "Eric Arthur Blair"); 
        }

        [Fact]
        public void Handle_ShouldThrowKeyNotFoundException_WhenAuthorDoesNotExist()
        {
            // Arrange
            var db = new FakeDatabase();
            var handler = new UpdateAuthorCommandHandler(db);
            var command = new UpdateAuthorCommand
            {
                Id = 99, 
                NewName = "Nuevo Nombre"
            };

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => handler.Handle(command));
        }

        [Fact]
        public void Handle_ShouldThrowArgumentException_WhenNewNameIsEmpty()
        {
            // Arrange
            var db = new FakeDatabase();
            db.Authors.Add(new Author { Id = 1, Name = "George Orwell" });
            var handler = new UpdateAuthorCommandHandler(db);
            var command = new UpdateAuthorCommand
            {
                Id = 1,
                NewName = "" 
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => handler.Handle(command));
        }
    }
}

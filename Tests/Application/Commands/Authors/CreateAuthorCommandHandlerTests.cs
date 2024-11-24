using Xunit; 
using Application.Commands.Authors; 
using Domain; 
using Infrastructure.Data;
using Application.Handler;

namespace Library.Tests.Application.Commands.Authors
{
    public class CreateAuthorCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldCreateAuthorSuccessfully()
        {
            // Arrange
            var db = new FakeDatabase(); // Use FakeDb
            var handler = new CreateAuthorCommandHandler(db);
            var command = new CreateAuthorCommand
            {
                Name = "J.K. Rowling"
            };

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.True(result); 
            Assert.Contains(db.Authors, a => a.Name == "J.K. Rowling"); // Verificate that the Author was created succesfully
        }

        [Fact]
        public void Handle_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
            // Arrange
            var db = new FakeDatabase();
            var handler = new CreateAuthorCommandHandler(db);
            var command = new CreateAuthorCommand
            {
                Name = "" // Nombre vacío
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => handler.Handle(command));
        }
    }
}


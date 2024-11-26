using Xunit;
using API.Commands.Authors;
using Domain;
using Infrastructure.Data;
using API.Handler;

namespace Library.Tests.API.Commands.Authors
{
    public class DeleteAuthorCommandHandlerTests
    {
        [Fact]
        public void Handle_ShouldDeleteAuthorSuccessfully()
        {
            // Arrange
            var db = new FakeDatabase();
            db.Authors.Add(new Author { Id = 1, Name = "George Orwell" }); // Author exist
            var handler = new DeleteAuthorCommandHandler(db);
            var command = new DeleteAuthorCommand
            {
                Id = 1
            };

            // Act
            var result = handler.Handle(command);

            // Assert
            Assert.True(result);  
            Assert.DoesNotContain(db.Authors, a => a.Id == 1);  
        }

        [Fact]
        public void Handle_ShouldThrowKeyNotFoundException_WhenAuthorDoesNotExist()
        {
            // Arrange
            var db = new FakeDatabase();
            var handler = new DeleteAuthorCommandHandler(db);
            var command = new DeleteAuthorCommand
            {
                Id = 99 
            };

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => handler.Handle(command));
        }
    }
}

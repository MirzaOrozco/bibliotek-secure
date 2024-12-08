using Application.Commands.Authors;
using Application.DataTransferObjects.Authors;
using Infrastructure.Data;

namespace Tests.AuthorTests.CommandTest.Update
{
    public class UpdateAuthorTests
    {
        private UpdateAuthorCommandHandler _handler;
        private FakeDatabase _db;

        public UpdateAuthorTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new UpdateAuthorCommandHandler(_db);
        }

        [Fact]
        public async Task ValidAuthorId_UpdatesAuthor()
        {
            // Arrange
            AuthorDto updatedAuthor = new AuthorDto
            {
                Name = "New Test Author Name",
            };
            Guid AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");
            var command = new UpdateAuthorCommand(updatedAuthor, AuthorId);
            int oldCount = _db.Authors.Count;

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedAuthor.Name, result.Name);
            Assert.Equal(oldCount, _db.Authors.Count);
            // Verify that Author was updated
            Assert.Contains(_db.Authors, a => a.Name == updatedAuthor.Name && a.Id == AuthorId);
            // Verify that Author is still unique
            Assert.Single(_db.Authors.FindAll(a => a.Id == AuthorId));
        }

        [Fact]
        public async Task InvalidAuthorId_ThrowsKeyNotFoundException()
        {
            // Arrange
            AuthorDto updatedAuthor = new AuthorDto
            {
                Name = "New Test Title 2"
            };
            Guid AuthorId = Guid.NewGuid();
            var command = new UpdateAuthorCommand(updatedAuthor, AuthorId);

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(action);
        }

        [Fact]
        public async Task InvalidName_ThrowsArgumentException()
        {
            // Arrange
            AuthorDto updatedAuthor = new AuthorDto
            {
                Name = String.Empty
            };
            Guid AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");
            var command = new UpdateAuthorCommand(updatedAuthor, AuthorId);

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
        }
    }
}

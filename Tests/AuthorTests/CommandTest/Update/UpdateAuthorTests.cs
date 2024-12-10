using Application.Commands.Authors;
using Application.DataTransferObjects.Authors;
using Infrastructure.Data;
using Infrastructure.Repository;
using Tests.MemoryDatabase;

namespace Tests.AuthorTests.CommandTest.Update
{
    public class UpdateAuthorTests
    {
        private UpdateAuthorCommandHandler _handler;
        private RealDatabase _db;

        public UpdateAuthorTests()
        {
            // Initialize the handler and fake database
            _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new UpdateAuthorCommandHandler(new AuthorRepository(_db));
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
            int oldCount = _db.Authors.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(updatedAuthor.Name, result.Name);
            Assert.Equal(oldCount, _db.Authors.Count());
            // Verify that Author was updated
            Assert.Contains(_db.Authors, a => a.Name == updatedAuthor.Name && a.Id == AuthorId);
            // Verify that Author is still unique
            Assert.Single(_db.Authors.Where(a => a.Id == AuthorId).ToList());
        }

        [Fact]
        public async Task InvalidAuthorId_KeyNotFound()
        {
            // Arrange
            AuthorDto updatedAuthor = new AuthorDto
            {
                Name = "New Test Title 2"
            };
            Guid AuthorId = Guid.NewGuid();
            var command = new UpdateAuthorCommand(updatedAuthor, AuthorId);

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
        }

        [Fact]
        public async Task InvalidName_Failure()
        {
            // Arrange
            AuthorDto updatedAuthor = new AuthorDto
            {
                Name = String.Empty
            };
            Guid AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");
            var command = new UpdateAuthorCommand(updatedAuthor, AuthorId);

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
        }
    }
}

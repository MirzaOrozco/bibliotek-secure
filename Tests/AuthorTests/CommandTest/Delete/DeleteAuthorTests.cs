
using Application.Commands.Authors;
using Application.DataTransferObjects.Authors;
using Infrastructure.Data;

namespace Tests.AuthorTests.CommandTest.Delete
{
    public class DeleteAuthorTests
    {
        private DeleteAuthorCommandHandler _handler;
        private FakeDatabase _db;

        public DeleteAuthorTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new DeleteAuthorCommandHandler(_db);
        }

        [Fact]
        public async Task AuthorWithoutBooks_IsRemoved()
        {
            // Arrange
            Guid AuthorId = new Guid("12345678-1234-5678-1234-a0000000000a");
            var command = new DeleteAuthorCommand(AuthorId);
            int oldCount = _db.Authors.Count;

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(AuthorId, result.Id);
            Assert.Equal(oldCount - 1, _db.Authors.Count);
            Assert.DoesNotContain(_db.Authors, a => a.Id == AuthorId);
        }

        [Fact]
        public async Task AuthorWithBooks_Failure()
        {
            // Arrange
            Guid AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");
            var command = new DeleteAuthorCommand(AuthorId);
            int oldCount = _db.Authors.Count;

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.Equal(oldCount, _db.Authors.Count);
        }

        [Fact]
        public async Task InvalidAuthorId_KeyNotFound()
        {
            // Arrange
            Guid AuthorId = Guid.NewGuid();
            var command = new DeleteAuthorCommand(AuthorId);
            int oldCount = _db.Authors.Count;

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
            Assert.Equal(oldCount, _db.Authors.Count);
        }
    }
}
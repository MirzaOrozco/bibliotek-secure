
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
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(AuthorId, result.Id);
            Assert.Equal(oldCount - 1, _db.Authors.Count);
            Assert.DoesNotContain(_db.Authors, a => a.Id == AuthorId);
        }

        [Fact]
        public async Task AuthorWithBooks_ThrowsArgumentException()
        {
            // Arrange
            Guid AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");
            var command = new DeleteAuthorCommand(AuthorId);
            int oldCount = _db.Authors.Count;

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Equal(oldCount, _db.Authors.Count);
        }

        [Fact]
        public async Task InvalidAuthorId_ThrowsKeyNotFoundException()
        {
            // Arrange
            Guid AuthorId = Guid.NewGuid();
            var command = new DeleteAuthorCommand(AuthorId);
            int oldCount = _db.Authors.Count;

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(action);
            Assert.Equal(oldCount, _db.Authors.Count);
        }
    }
}
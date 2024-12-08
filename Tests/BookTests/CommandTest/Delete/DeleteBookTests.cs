
using Application.Commands.Books;
using Application.DataTransferObjects.Books;
using Infrastructure.Data;

namespace Tests.BookTests.CommandTest.Delete
{
    public class DeleteBookTests
    {
        private DeleteBookCommandHandler _handler;
        private FakeDatabase _db;

        public DeleteBookTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new DeleteBookCommandHandler(_db);
        }

        [Fact]
        public async Task ValidBookId_IsRemoved()
        {
            // Arrange
            Guid bookId = new Guid("12345678-1234-5678-1234-b00000000000");
            var command = new DeleteBookCommand(bookId);
            int oldCount = _db.Books.Count;

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(oldCount - 1, _db.Books.Count);
            Assert.DoesNotContain(_db.Books, b => b.Id == bookId);
        }

        [Fact]
        public async Task InvalidBookId_KeyNotFound()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            var command = new DeleteBookCommand(bookId);
            int oldCount = _db.Books.Count;

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
            Assert.Equal(oldCount, _db.Books.Count);
        }
    }
}
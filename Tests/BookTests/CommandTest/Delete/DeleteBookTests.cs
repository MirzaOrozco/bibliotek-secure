
using Application.Commands.Books;
using Infrastructure.Data;
using Infrastructure.Repository;
using Tests.MemoryDatabase;

namespace Tests.BookTests.CommandTest.Delete
{
    public class DeleteBookTests
    {
        private DeleteBookCommandHandler _handler;
        private RealDatabase _db;

        public DeleteBookTests()
        {
            _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new DeleteBookCommandHandler(new BookRepository(_db));
        }

        [Fact]
        public async Task ValidBookId_IsRemoved()
        {
            // Arrange
            Guid bookId = new Guid("12345678-1234-5678-1234-b00000000000");
            var command = new DeleteBookCommand(bookId);
            int oldCount = _db.Books.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(oldCount - 1, _db.Books.Count());
            Assert.DoesNotContain(_db.Books, b => b.Id == bookId);
        }

        [Fact]
        public async Task InvalidBookId_KeyNotFound()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            var command = new DeleteBookCommand(bookId);
            int oldCount = _db.Books.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
            Assert.Equal(oldCount, _db.Books.Count());
        }
    }
}
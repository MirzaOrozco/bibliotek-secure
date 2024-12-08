
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
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(oldCount - 1, _db.Books.Count);
            Assert.DoesNotContain(_db.Books, b => b.Id == bookId);
        }

        [Fact]
        public async Task InvalidBookId_ThrowsKeyNotFoundException()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            var command = new DeleteBookCommand(bookId);
            int oldCount = _db.Books.Count;

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(action);
            Assert.Equal(oldCount, _db.Books.Count);
        }
    }
}
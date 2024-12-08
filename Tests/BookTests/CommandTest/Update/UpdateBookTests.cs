using Application.Commands.Books;
using Application.DataTransferObjects.Books;
using Infrastructure.Data;

namespace Tests.BookTests.CommandTest.Update
{
    public class UpdateBookTests
    {
        private UpdateBookCommandHandler _handler;
        private FakeDatabase _db;

        public UpdateBookTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new UpdateBookCommandHandler(_db);
        }

        [Fact]
        public async Task ValidTitleAndAuthorAndBookId_UpdatesBook()
        {
            // Arrange
            BookDto updatedBook = new BookDto
            {
                Title = "New Test Title",
                AuthorId = new Guid("12345678-1234-5678-1234-a00000000000")
            };
            Guid bookId = new Guid("12345678-1234-5678-1234-b00000000000");
            var command = new UpdateBookCommand(updatedBook, bookId);
            int oldCount = _db.Books.Count;

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedBook.Title, result.Title);
            Assert.Equal(oldCount, _db.Books.Count);
            // Verify that book was updated
            Assert.Contains(_db.Books, b => b.Title == updatedBook.Title && b.AuthorId == updatedBook.AuthorId && b.Id == bookId);
            // Verify that book is still unique
            Assert.Single(_db.Books.FindAll(b => b.Id == bookId));
        }

        [Fact]
        public async Task InvalidBookId_ThrowsKeyNotFoundException()
        {
            // Arrange
            BookDto updatedBook = new BookDto
            {
                Title = "New Test Title 2",
                AuthorId = new Guid("12345678-1234-5678-1234-a00000000000")
            };
            Guid bookId = Guid.NewGuid();
            var command = new UpdateBookCommand(updatedBook, bookId);

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(action);
        }

        [Fact]
        public async Task InvalidTitle_ThrowsArgumentException()
        {
            // Arrange
            BookDto updatedBook = new BookDto
            {
                Title = String.Empty,
                AuthorId = new Guid("12345678-1234-5678-1234-a00000000000")
            };
            Guid bookId = new Guid("12345678-1234-5678-1234-b00000000000");
            var command = new UpdateBookCommand(updatedBook, bookId);

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
        }

        [Fact]
        public async Task InvalidAuthor_ThrowsArgumentException()
        {
            // Arrange
            BookDto updatedBook = new BookDto
            {
                Title = "Test2",
                AuthorId = Guid.NewGuid()
            };
            Guid bookId = new Guid("12345678-1234-5678-1234-b00000000000");
            var command = new UpdateBookCommand(updatedBook, bookId);

            // Act
            var action = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
        }

    }
}

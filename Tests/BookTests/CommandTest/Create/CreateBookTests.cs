using Infrastructure.Data;
using Application.Commands.Books;
using Application.DataTransferObjects.Books;
using Application.Queries.Authors;
using Infrastructure.Repository;
using Tests.MemoryDatabase;

namespace Tests.BookTests.CommandTest.Create
{
    public class CreateBookTests
    {
        private CreateBookCommandHandler _handler;
        private RealDatabase _db;

        public CreateBookTests()
        {
            _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new CreateBookCommandHandler(new BookRepository(_db));
        }

        [Fact]
        public async Task ValidTitleAndAuthor_CreatesBook()
        {
            // Arrange
            BookDto newBook = new BookDto
            {
                Title = "Test",
                AuthorId = new Guid("12345678-1234-5678-1234-a00000000000")
            };
            var command = new CreateBookCommand(newBook);
            int oldCount = _db.Books.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(newBook.Title, result.Title);
            Assert.Equal(oldCount + 1, _db.Books.Count());
            // Verify that book was created
            Assert.Contains(_db.Books, b => b.Title == newBook.Title && b.AuthorId == newBook.AuthorId);
        }

        [Fact]
        public async Task InvalidTitleAndValidAuthor_Failure()
        {
            // Arrange
            BookDto newBook = new BookDto
            {
                Title = String.Empty,
                AuthorId = new Guid("12345678-1234-5678-1234-a00000000000")
            };
            var command = new CreateBookCommand(newBook);
            int oldCount = _db.Books.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.Equal(oldCount, _db.Books.Count());
        }

        [Fact]
        public async Task ValidTitleAndInvalidAuthor_Failure()
        {
            // Arrange
            BookDto newBook = new BookDto
            {
                Title = "Test2",
                AuthorId = Guid.NewGuid()
            };
            var command = new CreateBookCommand(newBook);
            int oldCount = _db.Books.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.Equal(oldCount, _db.Books.Count());
        }

        [Fact]
        public async Task InvalidTitleAndInvalidAuthor_Failure()
        {
            // Arrange
            BookDto newBook = new BookDto
            {
                Title = "",
                AuthorId = Guid.NewGuid()
            };
            var command = new CreateBookCommand(newBook);
            int oldCount = _db.Books.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.Equal(oldCount, _db.Books.Count());
        }
    }
}

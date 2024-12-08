using Infrastructure.Data;
using Application.Queries.Books;

namespace Tests.BookTests.QueryTest.GetByAuthorId
{
    public class GetBooksByAuthorIdTests
    {
        private GetBooksByAuthorIdQueryHandler _handler;
        private FakeDatabase _db;

        public GetBooksByAuthorIdTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new GetBooksByAuthorIdQueryHandler(_db);
        }

        [Fact]
        public async Task ValidId_ReturnsCorrectBooks()
        {
            // Arrange
            var AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");

            var query = new GetBooksByAuthorIdQuery(AuthorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(AuthorId, result[0].AuthorId);
            Assert.Equal(AuthorId, result[1].AuthorId);
            Assert.NotEqual(result[0].Id, result[1].Id);
        }

        [Fact]
        public async Task InvalidId_ReturnsEmpty()
        {
            // Arrange
            var invalidAuthorId = Guid.NewGuid();

            var query = new GetBooksByAuthorIdQuery(invalidAuthorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }
    }
}
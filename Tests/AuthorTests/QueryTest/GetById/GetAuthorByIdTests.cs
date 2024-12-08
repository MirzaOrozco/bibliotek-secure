using Application.Queries.Authors;
using Infrastructure.Data;

namespace Tests.AuthorTests.QueryTest.GetById
{
    public class GetAuthorByIdTests
    {
        private GetAuthorByIdQueryHandler _handler;
        private FakeDatabase _db;

        public GetAuthorByIdTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new GetAuthorByIdQueryHandler(_db);
        }

        [Fact]
        public async Task ValidId_ReturnsAuthor()
        {
            // Arrange
            var AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");
            var query = new GetAuthorByIdQuery(AuthorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(AuthorId, result.Id);
        }

        [Fact]
        public async Task InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidAuthorId = Guid.NewGuid();
            var query = new GetAuthorByIdQuery(invalidAuthorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}

using Infrastructure.Data;
using Application.Queries.Books;
using Domain;
using Infrastructure.Repository;
using Tests.MemoryDatabase;

namespace Tests.BookTests.QueryTest.GetByAuthorId
{
    public class GetBooksByAuthorIdTests
    {
        private GetBooksByAuthorIdQueryHandler _handler;
        private RealDatabase _db;

        public GetBooksByAuthorIdTests()
        {
            _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new GetBooksByAuthorIdQueryHandler(new BookRepository(_db));
        }

        [Fact]
        public async Task ValidId_ReturnsCorrectBooks()
        {
            // Arrange
            var AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");

            var query = new GetBooksByAuthorIdQuery(AuthorId);

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(AuthorId, result[0].AuthorId);
            Assert.Equal(AuthorId, result[1].AuthorId);
            Assert.NotEqual(result[0].Id, result[1].Id);
        }

        [Fact]
        public async Task InvalidId_Failure()
        {
            // Arrange
            var invalidAuthorId = Guid.NewGuid();

            var query = new GetBooksByAuthorIdQuery(invalidAuthorId);

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
        }
    }
}
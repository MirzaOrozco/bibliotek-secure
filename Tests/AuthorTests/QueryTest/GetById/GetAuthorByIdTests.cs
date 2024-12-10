using Application.Queries.Authors;
using Infrastructure.Data;
using Infrastructure.Repository;
using Tests.MemoryDatabase;

namespace Tests.AuthorTests.QueryTest.GetById
{
    public class GetAuthorByIdTests
    {
        private GetAuthorByIdQueryHandler _handler;
        private RealDatabase _db;

        public GetAuthorByIdTests()
        {
            _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new GetAuthorByIdQueryHandler(new AuthorRepository(_db));
        }

        [Fact]
        public async Task ValidId_ReturnsAuthor()
        {
            // Arrange
            var AuthorId = new Guid("12345678-1234-5678-1234-a00000000000");
            var query = new GetAuthorByIdQuery(AuthorId);

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.Equal(AuthorId, result.Id);
        }

        [Fact]
        public async Task InvalidId_KeyNotFound()
        {
            // Arrange
            var invalidAuthorId = Guid.NewGuid();
            var query = new GetAuthorByIdQuery(invalidAuthorId);

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
        }
    }
}

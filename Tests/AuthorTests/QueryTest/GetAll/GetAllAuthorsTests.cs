using Infrastructure.Data;
using Application.Queries.Authors;
using Tests.MemoryDatabase;
using Infrastructure.Repository;

namespace Tests.AuthorTests.QueryTest.GetAll
{
    public class GetAllAuthorsTests
    {
        private GetAllAuthorsQueryHandler _handler;
        private RealDatabase _db;

        public GetAllAuthorsTests()
        {
            _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new GetAllAuthorsQueryHandler(new AuthorRepository(_db));
        }

        [Fact]
        public async Task ReturnsAllAuthors()
        {
            // Arrange
            var query = new GetAllAuthorsQuery();

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.Equal(_db.Authors.Count(), result.Count);
        }
    }
}

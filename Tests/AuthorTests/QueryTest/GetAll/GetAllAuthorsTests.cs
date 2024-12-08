using Infrastructure.Data;
using Application.Queries.Authors;

namespace Tests.AuthorTests.QueryTest.GetAll
{
    public class GetAllAuthorsTests
    {
        private GetAllAuthorsQueryHandler _handler;
        private FakeDatabase _db;

        public GetAllAuthorsTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new GetAllAuthorsQueryHandler(_db);
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
            Assert.Equal(_db.Authors.Count, result.Count);
        }
    }
}

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
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_db.Authors.Count, result.Count);
        }
    }
}

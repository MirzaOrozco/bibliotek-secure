using Infrastructure.Data;
using Application.Queries.Books;

namespace Tests.BookTests.QueryTest.GetAll
{
    public class GetAllBooksTests
    {
        private GetAllBooksQueryHandler _handler;
        private FakeDatabase _db;

        public GetAllBooksTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new GetAllBooksQueryHandler(_db);
        }

        [Fact]
        public async Task ReturnsAllBooks()
        {
            // Arrange
            var query = new GetAllBooksQuery();

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.Equal(_db.Books.Count, result.Count);
        }
    }
}

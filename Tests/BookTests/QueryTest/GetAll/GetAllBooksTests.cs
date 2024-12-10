using Infrastructure.Data;
using Application.Queries.Books;
using Infrastructure.Repository;
using Tests.MemoryDatabase;

namespace Tests.BookTests.QueryTest.GetAll
{
    public class GetAllBooksTests
    {
        private GetAllBooksQueryHandler _handler;
        private RealDatabase _db;

        public GetAllBooksTests()
        {
            _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new GetAllBooksQueryHandler(new BookRepository(_db));
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
            Assert.Equal(_db.Books.Count(), result.Count);
        }
    }
}

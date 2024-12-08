using Application.Queries.Books;
using Infrastructure.Data;

namespace Tests.BookTests.QueryTest.GetById
{
    public class GetBookByIdTests
    {
        private GetBookByIdQueryHandler _handler;
        private FakeDatabase _db;

        public GetBookByIdTests()
        {
            // Initialize the handler and fake database
            _db = new FakeDatabase();
            _handler = new GetBookByIdQueryHandler(_db);
        }

        [Fact]
        public async Task ValidId_ReturnsBook()
        {
            // Arrange
            var BookId = new Guid("12345678-1234-5678-1234-b00000000000");
            var query = new GetBookByIdQuery(BookId);

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.Equal(BookId, result.Id);
        }

        [Fact]
        public async Task InvalidId_KeyNotFound()
        {
            // Arrange
            var invalidBookId = Guid.NewGuid();
            var query = new GetBookByIdQuery(invalidBookId);

            // Act
            var operationResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
        }
    }
}

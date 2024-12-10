using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Books;
using FakeItEasy;
using Domain;

namespace Tests.BookTests.QueryTest.GetById
{
    public class GetBookByIdTests
    {
        [Fact]
        public async Task ValidId_ReturnsBook()
        {
            // Arrange
            var BookId = Guid.NewGuid();
            var testBook = new Book { Id = BookId, Title = "TestTitle", AuthorId = Guid.NewGuid() };

            // Create fake repository that return the test book
            var fakeRepository = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepository.GetById(BookId))
                .Returns(OperationResult<Book>.Successful(testBook));

            var handler = new GetBookByIdQueryHandler(fakeRepository);
            var query = new GetBookByIdQuery(BookId);

            // Act
            var operationResult = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.Equal(testBook, result);
        }

        [Fact]
        public async Task InvalidId_KeyNotFound()
        {
            // Arrange
            var invalidBookId = Guid.NewGuid();
            // Create fake repository that returns key not found
            var fakeRepository = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepository.GetById(invalidBookId))
                .Returns(OperationResult<Book>.KeyNotFound(invalidBookId));

            var handler = new GetBookByIdQueryHandler(fakeRepository);
            var query = new GetBookByIdQuery(invalidBookId);

            // Act
            var operationResult = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
        }
    }
}

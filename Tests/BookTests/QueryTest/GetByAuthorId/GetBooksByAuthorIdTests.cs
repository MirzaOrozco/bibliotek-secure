using Application.Queries.Books;
using Domain;
using Application.Interfaces.RepositoryInterfaces;
using FakeItEasy;

namespace Tests.BookTests.QueryTest.GetByAuthorId
{
    public class GetBooksByAuthorIdTests
    {
        [Fact]
        public async Task ValidId_ReturnsCorrectBooks()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var testBooks = new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Book Part 1", AuthorId = authorId },
                new Book { Id = Guid.NewGuid(), Title = "Book Part 2", AuthorId = authorId },
                new Book { Id = Guid.NewGuid(), Title = "Book Part 3", AuthorId = authorId },
            };

            // Create fake repository that return the test books
            var fakeRepository = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepository.GetAllByAuthor(authorId))
                .Returns(OperationResult<List<Book>>.Successful(testBooks));

            var handler = new GetBooksByAuthorIdQueryHandler(fakeRepository);
            var query = new GetBooksByAuthorIdQuery(authorId);

            // Act
            var operationResult = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(result, testBooks);
        }

        [Fact]
        public async Task InvalidId_KeyNotFound()
        {
            // Arrange
            var invalidAuthorId = Guid.NewGuid();

            // Create fake repository that return key not found
            var fakeRepository = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepository.GetAllByAuthor(invalidAuthorId))
                .Returns(OperationResult<List<Book>>.KeyNotFound(invalidAuthorId));

            var handler = new GetBooksByAuthorIdQueryHandler(fakeRepository);
            var query = new GetBooksByAuthorIdQuery(invalidAuthorId);

            // Act
            var operationResult = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.True(operationResult.IsKeyNotFound);
        }
    }
}
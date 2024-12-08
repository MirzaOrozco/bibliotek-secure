using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Queries.Books
{
    public class GetBooksByAuthorIdQueryHandler : IRequestHandler<GetBooksByAuthorIdQuery, OperationResult<List<Book>>>
    {
        private readonly FakeDatabase _db;

        public GetBooksByAuthorIdQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<List<Book>>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            var books = _db.Books.FindAll(book => book.AuthorId == request.Id);
            if (books.Count == 0)
            {
                if (_db.Authors.Find(author => author.Id == request.Id) == null)
                {
                    return OperationResult<List<Book>>.KeyNotFound(request.Id, "There are no author with Id={0} in the library");
                }
                return OperationResult<List<Book>>.Failure("There are no books by that author in the library");
            }
            return OperationResult<List<Book>>.Successful(books, String.Format("Found {0} books by author Id {1}", books.Count, request.Id));
        }
    }
}

using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Queries.Books
{
    public class GetBooksByAuthorIdQueryHandler : IRequestHandler<GetBooksByAuthorIdQuery, List<Book>>
    {
        private readonly FakeDatabase _db;

        public GetBooksByAuthorIdQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<List<Book>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            var books = _db.Books.FindAll(book => book.AuthorId == request.Id);
            return Task.FromResult(books);
        }
    }
}

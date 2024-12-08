using Infrastructure.Data;
using MediatR;
using Domain;

namespace Application.Queries.Books
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly FakeDatabase _db;

        public GetBookByIdQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var foundBook = _db.Books.FirstOrDefault(book => book.Id == request.Id)!;
            return Task.FromResult(foundBook);
        }
    }

}

using Infrastructure.Data;
using MediatR;
using Domain;

namespace Application.Queries.Books
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {
        private readonly FakeDatabase _db;

        public GetBookByIdQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var foundBook = _db.Books.Find(book => book.Id == request.Id);
            if (foundBook == null)
            {
                return OperationResult<Book>.KeyNotFound(request.Id);
            }
            return OperationResult<Book>.Successful(foundBook);
        }
    }

}

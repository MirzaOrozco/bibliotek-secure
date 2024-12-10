using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Queries.Books
{
    public class GetBooksByAuthorIdQueryHandler : IRequestHandler<GetBooksByAuthorIdQuery, OperationResult<List<Book>>>
    {
        private readonly IBookRepository _db;

        public GetBooksByAuthorIdQueryHandler(IBookRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<List<Book>>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            return _db.GetAllByAuthor(request.Id);
        }
    }
}

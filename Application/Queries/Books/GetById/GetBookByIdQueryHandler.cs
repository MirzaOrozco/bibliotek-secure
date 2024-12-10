using MediatR;
using Domain;
using Application.Interfaces.RepositoryInterfaces;

namespace Application.Queries.Books
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {
        private readonly IBookRepository _db;

        public GetBookByIdQueryHandler(IBookRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return _db.GetById(request.Id);
        }
    }

}

using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Queries.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResult<List<Book>>>
    {
        private readonly IBookRepository _db;

        public GetAllBooksQueryHandler(IBookRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return _db.GetAll();
        }
    }
}

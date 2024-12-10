using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<List<Author>>>
    {
        private readonly IAuthorRepository _db;

        public GetAllAuthorsQueryHandler(IAuthorRepository db)
        {
            _db = db;
        }
        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return _db.GetAll();
        }
    }
}

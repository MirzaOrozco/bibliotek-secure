using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, OperationResult<Author>>
    {
        private readonly FakeDatabase _db;

        public GetAuthorByIdQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var foundAuthor = _db.Authors.Find(author => author.Id == request.Id);
            if (foundAuthor == null)
            {
                return OperationResult<Author>.KeyNotFound(request.Id);
            }
            return OperationResult<Author>.Successful(foundAuthor);
        }
    }
}

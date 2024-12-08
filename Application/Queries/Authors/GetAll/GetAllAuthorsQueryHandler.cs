using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly FakeDatabase _db;

        public GetAllAuthorsQueryHandler(FakeDatabase db)
        {
            _db = db;
        }
        public Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = _db.Authors;
            return Task.FromResult(authors);
        }
    }
}

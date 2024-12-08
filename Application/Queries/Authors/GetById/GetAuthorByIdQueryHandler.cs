using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly FakeDatabase _db;

        public GetAuthorByIdQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var foundAuthor = _db.Authors.FirstOrDefault(author => author.Id == request.Id)!;
            return Task.FromResult(foundAuthor);
        }
    }
}

using Domain;
using Infrastructure.Data;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Authors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<List<Author>>>
    {
        private readonly FakeDatabase _db;

        public GetAllAuthorsQueryHandler(FakeDatabase db)
        {
            _db = db;
        }
        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = _db.Authors;
            return OperationResult<List<Author>>.Successful(authors, String.Format("Found {0} authors", authors.Count));
        }
    }
}

using Domain;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAuthorByIdQuery : IRequest<OperationResult<Author>>
    {
        public GetAuthorByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}

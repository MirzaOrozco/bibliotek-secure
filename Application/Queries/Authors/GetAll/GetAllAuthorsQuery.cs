using Domain;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAllAuthorsQuery : IRequest<OperationResult<List<Author>>>
    {
    }
}

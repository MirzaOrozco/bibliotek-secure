using Domain;
using MediatR;

namespace Application.Queries.Authors
{
    public class GetAllAuthorsQuery : IRequest<List<Author>>
    {
    }
}

using Domain;
using MediatR;

namespace Application.Queries.Books
{
    public class GetBooksByAuthorIdQuery : IRequest<List<Book>>
    {
        public GetBooksByAuthorIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}

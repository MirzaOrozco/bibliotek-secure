using Domain;
using MediatR;

namespace Application.Queries.Books
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public GetBookByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}

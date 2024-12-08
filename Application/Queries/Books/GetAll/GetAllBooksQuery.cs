using Domain;
using MediatR;

namespace Application.Queries.Books
{
    public class GetAllBooksQuery : IRequest<OperationResult<List<Book>>>
    {
    }
}

using Domain;
using MediatR;

namespace Application.Commands.Books
{
    public class DeleteBookCommand : IRequest<OperationResult<Book>>
    {
        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

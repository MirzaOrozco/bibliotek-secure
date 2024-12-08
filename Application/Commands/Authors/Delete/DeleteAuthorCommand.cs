using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class DeleteAuthorCommand : IRequest<OperationResult<Author>>
    {
        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

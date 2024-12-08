using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class DeleteAuthorCommand : IRequest<Author>
    {
        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

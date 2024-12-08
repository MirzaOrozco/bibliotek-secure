using Application.DataTransferObjects.Authors;
using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class UpdateAuthorCommand : IRequest<Author>
    {
        public UpdateAuthorCommand(AuthorDto updatedAuthor, Guid id)
        {
            UpdatedAuthor = updatedAuthor;
            Id = id;
        }

        public Guid Id { get; }
        public AuthorDto UpdatedAuthor { get; }
    }
}

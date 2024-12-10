using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _db;

        public UpdateAuthorCommandHandler(IAuthorRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<Author>> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.UpdatedAuthor.Name))
            {
                return OperationResult<Author>.Failure("The new Author's name can not be empty.");
            }

            return _db.Update(command.Id, command.UpdatedAuthor);
        }
    }
}

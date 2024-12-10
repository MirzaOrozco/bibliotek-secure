using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _db;

        public CreateAuthorCommandHandler(IAuthorRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<Author>> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.NewAuthor.Name))
            {
                return OperationResult<Author>.Failure("The name of the Author can not be empty");
            }

            return _db.Create(command.NewAuthor);
        }
    }
}

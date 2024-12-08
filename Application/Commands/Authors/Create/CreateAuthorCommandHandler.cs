using Infrastructure.Data;
using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, OperationResult<Author>>
    {
        private readonly FakeDatabase _db;

        public CreateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<Author>> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.NewAuthor.Name))
            {
                return OperationResult<Author>.Failure("The name of the Author can not be empty");
            }

            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = command.NewAuthor.Name
            };

            _db.Authors.Add(newAuthor);
            return OperationResult<Author>.Successful(newAuthor);
        }
    }
}

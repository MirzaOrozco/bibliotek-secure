using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Authors
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, OperationResult<Author>>
    {
        private readonly FakeDatabase _db;

        public UpdateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<Author>> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.UpdatedAuthor.Name))
            {
                return OperationResult<Author>.Failure("The new Author's name can not be empty.");
            }

            var author = _db.Authors.Find(a => a.Id == command.Id);
            if (author == null)
            {
                return OperationResult<Author>.KeyNotFound(command.Id);
            }

            author.Name = command.UpdatedAuthor.Name;
            return OperationResult<Author>.Successful(author);
        }
    }
}

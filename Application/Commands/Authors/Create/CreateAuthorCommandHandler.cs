using Infrastructure.Data;
using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly FakeDatabase _db;

        public CreateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Author> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.NewAuthor.Name))
            {
                throw new ArgumentException("The name of the Author can not be empty");
            }

            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = command.NewAuthor.Name
            };

            _db.Authors.Add(newAuthor);
            return Task.FromResult(newAuthor);
        }
    }
}

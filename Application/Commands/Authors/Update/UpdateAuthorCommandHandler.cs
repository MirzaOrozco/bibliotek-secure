using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Authors
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly FakeDatabase _db;

        public UpdateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Author> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.UpdatedAuthor.Name))
            {
                throw new ArgumentException("The new Author's name can not be empty.");
            }

            var author = _db.Authors.Find(a => a.Id == command.Id);
            if (author == null)
            {
                throw new KeyNotFoundException("The Author doesn't exist.");
            }

            author.Name = command.UpdatedAuthor.Name;
            return Task.FromResult(author);
        }
    }
}

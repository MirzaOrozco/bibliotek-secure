using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Authors
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, OperationResult<Author>>
    {
        private readonly FakeDatabase _db;

        public DeleteAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<Author>> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = _db.Authors.Find(a => a.Id == command.Id);
            if (author == null)
            {
                return OperationResult<Author>.KeyNotFound(command.Id);
            }
            var book = _db.Books.Find(b => b.AuthorId == command.Id);
            if (book != null)
            {
                var booksWithAuthor = _db.Books.FindAll(b => b.AuthorId == command.Id);
                var formattedString = string.Format("There are {0} books by the author {1}, the books need to be deleted or updated first", booksWithAuthor.Count, author.Name);
                return OperationResult<Author>.Failure(formattedString);
            }
            _db.Authors.Remove(author);
            return OperationResult<Author>.Successful(author);
        }
    }
}


using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Books
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, OperationResult<Book>>
    {
        private readonly FakeDatabase _db;

        public UpdateBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<Book>> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            // Search for the book to update
            var book = _db.Books.Find(b => b.Id == command.Id);
            if (book == null)
            {
                return OperationResult<Book>.KeyNotFound(command.Id);
            }
            // Check if title is valid
            if (string.IsNullOrWhiteSpace(command.UpdatedBook.Title))
            {
                return OperationResult<Book>.Failure("The book title can not be empty");
            }
            // Check if there's an author with that id
            if (_db.Authors.Find(x => x.Id == command.UpdatedBook.AuthorId) == null)
            {
                return OperationResult<Book>.Failure("Author ID should be valid");
            }

            // Update Book
            book.Title = command.UpdatedBook.Title;
            book.AuthorId = command.UpdatedBook.AuthorId;
            return OperationResult<Book>.Successful(book);
        }
    }
}
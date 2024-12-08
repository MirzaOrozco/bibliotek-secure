using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Books
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResult<Book>>
    {
        private readonly FakeDatabase _db;

        public DeleteBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<Book>> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            // Search the book to delete
            var book = _db.Books.Find(b => b.Id == command.Id);
            if (book == null)
            {
                return OperationResult<Book>.KeyNotFound(command.Id);
            }

            // Delete the book
            _db.Books.Remove(book);
            return OperationResult<Book>.Successful(book);
        }
    }
}

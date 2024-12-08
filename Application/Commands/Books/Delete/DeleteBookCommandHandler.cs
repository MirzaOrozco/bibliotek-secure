using Domain;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Books
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly FakeDatabase _db;

        public DeleteBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public Task<Book> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            // Search the book to delete
            var book = _db.Books.Find(b => b.Id == command.Id);
            if (book == null)
            {
                throw new KeyNotFoundException("The book doesn't exist");
            }

            // Delete the book
            _db.Books.Remove(book);
            return Task.FromResult(book);
        }
    }
}

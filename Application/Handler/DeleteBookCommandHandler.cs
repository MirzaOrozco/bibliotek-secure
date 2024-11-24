using Application.Commands.Books;
using Infrastructure.Data;

namespace Application.Handler
{
    public class DeleteBookCommandHandler
    {
        private readonly FakeDatabase _db;

        public DeleteBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public bool Handle(DeleteBookCommand command)
        {
            // Verificate that the User is a employee
            var user = _db.Users.Find(u => u.Id == command.UserId);
            if (user == null || user.Name != "Librarian")
            {
                throw new UnauthorizedAccessException("Just authorizade personal can delete books");
            }

            // Search the book to delete
            var book = _db.Books.Find(b => b.Id == command.BookId);
            if (book == null)
            {
                throw new KeyNotFoundException("The book doesn't exist");
            }

            // Delete the book
            _db.Books.Remove(book);
            return true; // Shows that was deleted
        }
    }
}

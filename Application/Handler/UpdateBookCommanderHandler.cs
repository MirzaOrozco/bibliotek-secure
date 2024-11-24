using Application.Commands.Books;
using Infrastructure.Data;

namespace Application.Handler
{
    public class UpdateBookCommandHandler
    {
        private readonly FakeDatabase _db;

        public UpdateBookCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public bool Handle(UpdateBookCommand command)
        {
            // Verificate is authorize personal
            var user = _db.Users.Find(u => u.Id == command.UserId);
            if (user == null || user.Name != "Librarian")
            {
                throw new UnauthorizedAccessException("Just authorized personal");
            }

            // Search for the book to update
            var book = _db.Books.Find(b => b.Id == command.BookId);
            if (book == null)
            {
                throw new KeyNotFoundException("The book doesn't exist.");
            }

            // Update Book
            book.Title = command.NewTitle;
            book.AuthorId = command.NewAuthorId;
            return true; // Book update succesfully
        }
    }
}
using Application.Commands.Books;
using Domain;
using Infrastructure.Data;


namespace Application.Commands.Books
{
    public class CreateBookCommandHandler
    {
        private readonly FakeDatabase _db;

        public CreateBookCommandHandler(FakeDatabase db)
        {
            _db = db; // FakeDataBase
        }

        public bool Handle(CreateBookCommand command)
        {
            // Verificate the command have rights commands
            if (string.IsNullOrWhiteSpace(command.Title))
            {
                throw new ArgumentException("The book title can not be empty");
            }

            if (command.AuthorId <= 0)
            {
                throw new ArgumentException("Author ID shoulb be valid");
            }

            // Create a new book
            var newBook = new Book
            {
                Id = _db.Books.Count + 1, // Generate simple ID 
                Title = command.Title,
                AuthorId = command.AuthorId,
                IsReserved = false
            };

            // Add book to Db
            _db.Books.Add(newBook);

            // Return true to confirm
            return true;
        }
    }
}

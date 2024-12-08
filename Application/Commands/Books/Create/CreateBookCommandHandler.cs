using Domain;
using Infrastructure.Data;
using MediatR;


namespace Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly FakeDatabase _db;

        public CreateBookCommandHandler(FakeDatabase db)
        {
            _db = db; // FakeDataBase
        }

        public Task<Book> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            // Check if title is valid
            if (string.IsNullOrWhiteSpace(command.NewBook.Title))
            {
                throw new ArgumentException("The book title can not be empty");
            }

            // Check if there's an author with that id
            if (_db.Authors.Find(x => x.Id == command.NewBook.AuthorId) == null)
            {
                throw new ArgumentException("Author ID should be valid");
            }

            // Create a new book
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = command.NewBook.Title,
                AuthorId = command.NewBook.AuthorId
            };

            // Add book to Db
            _db.Books.Add(newBook);

            return Task.FromResult(newBook);
        }
    }
}

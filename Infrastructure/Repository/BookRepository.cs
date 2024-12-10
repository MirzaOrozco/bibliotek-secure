
using Application.DataTransferObjects.Books;
using Application.Interfaces.RepositoryInterfaces;
using Azure.Core;
using Domain;
using Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly RealDatabase _db;

        public BookRepository(RealDatabase db)
        {
            _db = db;
        }

        public OperationResult<Book> Create(BookDto dto)
        {
            // Check if there's an author with that id
            if (_db.Authors.Find(dto.AuthorId) == null)
            {
                return OperationResult<Book>.Failure("Author ID should be valid");
            }

            // Create a new book
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                AuthorId = dto.AuthorId
            };

            // Add book to Db
            _db.Books.Add(newBook);
            _db.SaveChanges();

            return OperationResult<Book>.Successful(newBook);
        }

        public OperationResult<Book> Delete(Guid id)
        {
            // Search the book to delete
            var book = _db.Books.Find(id);
            if (book == null)
            {
                return OperationResult<Book>.KeyNotFound(id);
            }

            // Delete the book
            _db.Books.Remove(book);
            _db.SaveChanges();
            return OperationResult<Book>.Successful(book);
        }

        public OperationResult<List<Book>> GetAll()
        {
            var books = _db.Books.OrderBy(book => book.Title).ToList();
            return OperationResult<List<Book>>.Successful(books, String.Format("Found {0} books", books.Count));
        }

        public OperationResult<List<Book>> GetAllByAuthor(Guid authorId)
        {
            var books = _db.Books.Where(book => book.AuthorId == authorId).OrderBy(book => book.Title).ToList();
            if (books.Count == 0)
            {
                if (_db.Authors.Find(authorId) == null)
                {
                    return OperationResult<List<Book>>.KeyNotFound(authorId, "There are no author with Id={0} in the library");
                }
                return OperationResult<List<Book>>.Failure("There are no books by that author in the library");
            }
            return OperationResult<List<Book>>.Successful(books, String.Format("Found {0} books by author Id {1}", books.Count, authorId));
        }

        public OperationResult<Book> GetById(Guid id)
        {
            var book = _db.Books.Find(id);
            if (book == null)
            {
                return OperationResult<Book>.KeyNotFound(id);
            }
            return OperationResult<Book>.Successful(book);
        }

        public OperationResult<Book> Update(Guid id, BookDto dto)
        {
            // Check if there's an author with that id
            if (_db.Authors.Find(dto.AuthorId) == null)
            {
                return OperationResult<Book>.Failure("Must update to a valid Author Id");
            }
            // Search for the book to update
            var book = _db.Books.Find(id);
            if (book == null)
            {
                return OperationResult<Book>.KeyNotFound(id);
            }
            book.Title = dto.Title;
            book.AuthorId = dto.AuthorId;
            _db.SaveChanges();
            return OperationResult<Book>.Successful(book);
        }
    }
}

using Application.DataTransferObjects.Authors;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly RealDatabase _db;

        public AuthorRepository(RealDatabase db)
        {
            _db = db;
        }
        public OperationResult<Author> Create(AuthorDto dto)
        {
            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
            _db.Authors.Add(newAuthor);
            _db.SaveChanges();
            return OperationResult<Author>.Successful(newAuthor, String.Format("Created Author {0}", newAuthor.Id));
        }

        public OperationResult<Author> Delete(Guid id)
        {
            var author = _db.Authors.Find(id);
            if (author == null)
            {
                return OperationResult<Author>.KeyNotFound(id);
            }
            var books = _db.Books.Where(b => b.AuthorId == id).ToList();
            if (books.Count > 0)
            {
                var formattedString = string.Format("There are {0} books by the author {1}, the books need to be deleted or updated first", books.Count, author.Name);
                return OperationResult<Author>.Failure(formattedString);
            }
            _db.Authors.Remove(author);
            _db.SaveChanges();
            return OperationResult<Author>.Successful(author, String.Format("Deleted Author {0}", id));
        }

        public OperationResult<List<Author>> GetAll()
        {
            var authors = _db.Authors.OrderBy(author => author.Name).ToList();
            return OperationResult<List<Author>>.Successful(authors, String.Format("Found {0} authors", authors.Count));
        }

        public OperationResult<Author> GetById(Guid id)
        {
            var author = _db.Authors.Find(id);
            if (author == null)
            {
                return OperationResult<Author>.KeyNotFound(id);
            }
            return OperationResult<Author>.Successful(author);
        }

        public OperationResult<Author> Update(Guid id, AuthorDto dto)
        {
            var author = _db.Authors.Find(id);
            if (author == null)
            {
                return OperationResult<Author>.KeyNotFound(id);
            }
            author.Name = dto.Name;
            _db.SaveChanges();
            return OperationResult<Author>.Successful(author);
        }
    }
}

using Application.DataTransferObjects.Books;
using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository : ICRUDRepository<Book, BookDto>
    {
        OperationResult<List<Book>> GetAllByAuthor(Guid authorId);
    }
}

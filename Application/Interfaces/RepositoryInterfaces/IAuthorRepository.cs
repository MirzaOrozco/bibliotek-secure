using Application.DataTransferObjects.Authors;
using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository : ICRUDRepository<Author, AuthorDto>
    {
    }
}

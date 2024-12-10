using Domain;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ICRUDRepository<T, DTO>
    {
        OperationResult<T> Create(DTO dto);

        // Read
        OperationResult<List<T>> GetAll();
        OperationResult<T> GetById(Guid id);

        OperationResult<T> Update(Guid id, DTO dto);

        OperationResult<T> Delete(Guid id);
    }
}

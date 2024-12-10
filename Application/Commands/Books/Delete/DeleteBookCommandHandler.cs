using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Commands.Books
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _db;

        public DeleteBookCommandHandler(IBookRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<Book>> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            return _db.Delete(command.Id);
        }
    }
}

using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _db;

        public DeleteAuthorCommandHandler(IAuthorRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<Author>> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            return _db.Delete(command.Id);
        }
    }
}


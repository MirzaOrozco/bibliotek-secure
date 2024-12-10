using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Commands.Books
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _db;

        public UpdateBookCommandHandler(IBookRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<Book>> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            // Check if title is valid
            if (string.IsNullOrWhiteSpace(command.UpdatedBook.Title))
            {
                return OperationResult<Book>.Failure("The book title can not be empty");
            }
            return _db.Update(command.Id, command.UpdatedBook);
        }
    }
}
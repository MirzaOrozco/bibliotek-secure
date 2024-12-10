using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;


namespace Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _db;

        public CreateBookCommandHandler(IBookRepository db)
        {
            _db = db;
        }

        public async Task<OperationResult<Book>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            // Check if title is valid
            if (string.IsNullOrWhiteSpace(command.NewBook.Title))
            {
                return OperationResult<Book>.Failure("The book title can not be empty");
            }

            return _db.Create(command.NewBook);
        }
    }
}

using Application.DataTransferObjects.Books;
using Domain;
using MediatR;

namespace Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public UpdateBookCommand(BookDto updatedBook, Guid id)
        {
            UpdatedBook = updatedBook;
            Id = id;
        }

        public Guid Id { get; }
        public BookDto UpdatedBook { get; }
    }
}

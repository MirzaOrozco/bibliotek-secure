using Application.DataTransferObjects.Books;
using Domain;
using MediatR;

namespace Application.Commands.Books
{
    public class CreateBookCommand : IRequest<Book>
    {
        public CreateBookCommand(BookDto newBook)
        {
            NewBook = newBook;
        }

        public BookDto NewBook { get; }
    }
}

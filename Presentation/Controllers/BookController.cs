using Microsoft.AspNetCore.Mvc;
using Application.Commands.Books;
using MediatR;
using Application.DataTransferObjects.Books;
using Application.Queries.Books;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all books from database
        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _mediator.Send(new GetAllBooksQuery()));
        }

        //Get a Book by Id
        [HttpGet]
        [Route("getBookId/{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            return Ok(await _mediator.Send(new GetBookByIdQuery(bookId)));
        }

        //Get all Books by Author Id
        [HttpGet]
        [Route("getBooksByAuthorId/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthorId(Guid authorId)
        {
            return Ok(await _mediator.Send(new GetBooksByAuthorIdQuery(authorId)));
        }

        // Add/Create New Book
        [HttpPost]
        [Route("createBook")]
        public async Task<IActionResult> CreateBook([FromBody] BookDto newBook)
        {
            return Ok(await _mediator.Send(new CreateBookCommand(newBook)));
        }

        [HttpDelete]
        [Route("deleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteBookCommand(id)));
            /*
            try
            {
                handler.Handle(command);
                return Ok("Book deleted succesfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            */
        }

        [HttpPut]
        [Route("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto updatedBook, Guid id)
        {
            return Ok(await _mediator.Send(new UpdateBookCommand(updatedBook, id)));
/*
            try
            {
                handler.Handle(command);
                return Ok("Book update succesfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
*/
        }
    }
}

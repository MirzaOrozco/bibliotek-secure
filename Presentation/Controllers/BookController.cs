using Microsoft.AspNetCore.Mvc;
using Application.Commands.Books;
using MediatR;
using Application.DataTransferObjects.Books;
using Application.Queries.Books;
using Domain;

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

        private IActionResult ReturnCode<T>(OperationResult<T> operationResult)
        {
            if (operationResult.IsSuccessful)
            {
                return Ok(new { message = operationResult.Message, data = operationResult.Data, details = operationResult.Details });
            }
            else if (operationResult.IsKeyNotFound)
            {
                return NotFound(new { message = operationResult.Message, errors = operationResult.Details });
            }
            else
            {
                return BadRequest(new { message = operationResult.Message, errors = operationResult.Details });
            }
        }

        // Get all books from database
        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAllBooksQuery());
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //Get a Book by Id
        [HttpGet]
        [Route("getBookId/{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetBookByIdQuery(bookId));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //Get all Books by Author Id
        [HttpGet]
        [Route("getBooksByAuthorId/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthorId(Guid authorId)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetBooksByAuthorIdQuery(authorId));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Add/Create New Book
        [HttpPost]
        [Route("createBook")]
        public async Task<IActionResult> CreateBook([FromBody] BookDto newBook)
        {
            try
            {
                var operationResult = await _mediator.Send(new CreateBookCommand(newBook));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("deleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new DeleteBookCommand(id));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto updatedBook, Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new UpdateBookCommand(updatedBook, id));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

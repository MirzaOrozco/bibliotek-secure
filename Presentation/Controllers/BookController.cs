using Microsoft.AspNetCore.Mvc;
using Application.Commands.Books;
using MediatR;
using Application.DataTransferObjects.Books;
using Application.Queries.Books;
using Domain;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private IActionResult ReturnCode<T>(OperationResult<T> operationResult)
        {
            if (operationResult.IsSuccessful)
            {
                if (operationResult.Details.IsNullOrEmpty())
                {
                    _logger.LogInformation("Return Ok");
                }
                else
                {
                    _logger.LogInformation("Return Ok: {Details}", operationResult.Details);
                }
                return Ok(new { message = operationResult.Message, data = operationResult.Data, details = operationResult.Details });
            }
            else if (operationResult.IsKeyNotFound)
            {
                _logger.LogWarning("Return NotFound for {Id}: {Error}", operationResult.KeyNotFoundGuid, operationResult.Details);
                return NotFound(new { message = operationResult.Message, errors = operationResult.Details });
            }
            else
            {
                _logger.LogWarning("Return BadRequest: {Error}", operationResult.Details);
                return BadRequest(new { message = operationResult.Message, errors = operationResult.Details });
            }
        }

        // Get all books from database
        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            _logger.LogInformation("Getting all books at {Time}", DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new GetAllBooksQuery());
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while getting all books");
                return StatusCode(500, "Internal server error");
            }
        }

        //Get a Book by Id
        [HttpGet]
        [Route("getBookId/{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            _logger.LogInformation("Getting book {Id} at {Time}", bookId, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new GetBookByIdQuery(bookId));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while getting book {Id}", bookId);
                return StatusCode(500, "Internal server error");
            }
        }

        //Get all Books by Author Id
        [HttpGet]
        [Route("getBooksByAuthorId/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthorId(Guid authorId)
        {
            _logger.LogInformation("Getting all books by author {AuthorId} at {Time}", authorId, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new GetBooksByAuthorIdQuery(authorId));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while getting books by author {AuthorId}", authorId);
                return StatusCode(500, "Internal server error");
            }
        }

        // Add/Create New Book
        [HttpPost]
        [Route("createBook")]
        public async Task<IActionResult> CreateBook([FromBody] BookDto newBook)
        {
            _logger.LogInformation("Creating book {BookTitle} at {Time}", newBook.Title, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new CreateBookCommand(newBook));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while creating book");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("deleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            _logger.LogInformation("Deleting book {Id} at {Time}", id, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new DeleteBookCommand(id));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while deleting book {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto updatedBook, Guid id)
        {
            _logger.LogInformation("Updating book {Id} to {BookTitle} at {Time}", id, updatedBook.Title, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new UpdateBookCommand(updatedBook, id));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while updating book {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Application.Commands.Authors;
using MediatR;
using Application.DataTransferObjects.Authors;
using Application.Queries.Authors;
using Domain;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
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

        // Get all Authors from database
        [HttpGet]
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAllAuthorsQuery());
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //Get a Author by Id
        [HttpGet]
        [Route("getAuthorId/{authorId}")]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAuthorByIdQuery(authorId));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Create a new Author
        [HttpPost]
        [Route("createAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto author)
        {
            try
            {
                var operationResult = await _mediator.Send(new CreateAuthorCommand(author));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("updateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorDto updatedAuthor, Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new UpdateAuthorCommand(updatedAuthor, id));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("deleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new DeleteAuthorCommand(id));
                return ReturnCode(operationResult);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

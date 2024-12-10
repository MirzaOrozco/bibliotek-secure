using Microsoft.AspNetCore.Mvc;
using Application.Commands.Authors;
using MediatR;
using Application.DataTransferObjects.Authors;
using Application.Queries.Authors;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorController> _logger; 

        public AuthorController(IMediator mediator, ILogger<AuthorController> logger)
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

        // Get all Authors from database
        [HttpGet]
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            _logger.LogInformation("Getting all authors at {Time}", DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new GetAllAuthorsQuery());
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while getting all authors");
                return StatusCode(500, "Internal server error");
            }
        }

        //Get a Author by Id
        [HttpGet]
        [Route("getAuthorId/{authorId}")]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            _logger.LogInformation("Getting author {Id} at {Time}", authorId, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new GetAuthorByIdQuery(authorId));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while getting author {Id}", authorId);
                return StatusCode(500, "Internal server error");
            }
        }

        // Create a new Author
        [HttpPost]
        [Route("createAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto author)
        {
            _logger.LogInformation("Creating author {AuthorName} at {Time}", author.Name, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new CreateAuthorCommand(author));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while creating author");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("updateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorDto updatedAuthor, Guid id)
        {
            _logger.LogInformation("Updating author {Id} to {AuthorName} at {Time}", id, updatedAuthor.Name, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new UpdateAuthorCommand(updatedAuthor, id));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while updating author {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("deleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            _logger.LogInformation("Deleting author {Id} at {Time}", id, DateTime.UtcNow);
            try
            {
                var operationResult = await _mediator.Send(new DeleteAuthorCommand(id));
                return ReturnCode(operationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error occured while deleting author {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

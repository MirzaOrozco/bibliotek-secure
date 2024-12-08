using Microsoft.AspNetCore.Mvc;
using Application.Commands.Authors;
using MediatR;
using Application.DataTransferObjects.Authors;
using Application.Queries.Authors;

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

        // Get all Authors from database
        [HttpGet]
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsQuery()));
        }

        //Get a Author by Id
        [HttpGet]
        [Route("getAuthorId/{authorId}")]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            return Ok(await _mediator.Send(new GetAuthorByIdQuery(authorId)));
        }

        // Create a new Author
        [HttpPost]
        [Route("createAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto author)
        {
            return Ok(await _mediator.Send(new CreateAuthorCommand(author)));
        }

        [HttpPut]
        [Route("updateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorDto updatedAuthor, Guid id)
        {
            return Ok(await _mediator.Send(new UpdateAuthorCommand(updatedAuthor, id)));
        }

        /* TODO: Exception handling like this for above?
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(Guid id, [FromBody] UpdateAuthorDto )
        {
            command.Id = id;
            var handler = new UpdateAuthorCommandHandler();
            try
            {
                handler.Handle(command);
                return Ok("Author update succesfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        */

        [HttpDelete]
        [Route("deleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteAuthorCommand(id)));
        }
        /*
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(Guid id)
        {
            var command = new DeleteAuthorCommand { Id = id };
            var handler = new DeleteAuthorCommandHandler();
            try
            {
                handler.Handle(command);
                return Ok("Author deleted succesfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        */
    }
}

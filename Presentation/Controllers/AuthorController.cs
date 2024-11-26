using Microsoft.AspNetCore.Mvc;
using API.Commands.Authors;
using Infrastructure.Data;
using API.Handler.Authors;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly FakeDatabase _db;

        public AuthorController()
        {
            _db = new FakeDatabase();
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var handler = new CreateAuthorCommandHandler(_db);
            try
            {
                handler.Handle(command);
                return Ok("Author created succesfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command)
        {
            command.Id = id;
            var handler = new UpdateAuthorCommandHandler(_db);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var command = new DeleteAuthorCommand { Id = id };
            var handler = new DeleteAuthorCommandHandler(_db);
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
    }
}

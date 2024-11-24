using Infrastructure.Data;         
using Domain;                     
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Books;
using Application.Handler;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly FakeDatabase _db;

        public BookController()
        {
            _db = new FakeDatabase(); //  Fake Database
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _db.Books.Find(b => b.Id == id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]

        public IActionResult CreateBook(CreateBookCommand command)
        {
            var newBook = new Book
            {
                Id = _db.Books.Count + 1,
                Title = command.Title,
                AuthorId = command.AuthorId
            };
            _db.Books.Add(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id, [FromQuery] int userId)
        {
            var command = new DeleteBookCommand { BookId = id, UserId = userId };
            var handler = new DeleteBookCommandHandler(_db);

            try
            {
                handler.Handle(command);
                return Ok("Libro eliminado exitosamente.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommand command)
        {
            command.BookId = id; // To be sure the Id Book it's right
            var handler = new UpdateBookCommandHandler(_db);

            try
            {
                handler.Handle(command);
                return Ok("Libro actualizado exitosamente.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

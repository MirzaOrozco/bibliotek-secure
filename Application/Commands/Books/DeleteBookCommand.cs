using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Books
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        public int UserId { get; set; } //ID user who ask to delete book
    }
}

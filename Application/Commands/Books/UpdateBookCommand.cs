using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Books
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public string NewTitle { get; set; }
        public int NewAuthorId { get; set; }
        public int UserId { get; set; } // Id user who make update
    }
}

using System.Collections.Generic;
using Domain;

namespace Infrastructure.Data
{
    public class FakeDatabase
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<User> Users { get; set; } = new List<User>();
    }
}

using Domain;

namespace Infrastructure.Data
{
    public class FakeDatabase
    {
        public List<Book> Books
        {
            get { return allBooks; }
            set { allBooks = value; }
        }

        public List<Author> Authors
        {
            get { return allAuthors; }
            set { allAuthors = value; }
        }

        public List<User> Users { get; set; } = new List<User>();

        private List<Author> allAuthors = new()
        {
            new Author { Id = new Guid("12345678-1234-5678-1234-a00000000000"), Name = "TestAuthorForUnitTests"},
            new Author { Id = new Guid("12345678-1234-5678-1234-a00000000001"), Name = "Steven King"},
            new Author { Id = new Guid("12345678-1234-5678-1234-a00000000002"), Name = "JRR Tolkien"},
            new Author { Id = new Guid("12345678-1234-5678-1234-a00000000003"), Name = "Gunilla Bergström"},
            new Author { Id = new Guid("12345678-1234-5678-1234-a0000000000a"), Name = "AuthorWithoutBooksForUnitTests"},
        };
        private List<Book> allBooks = new()
        {
            new Book { Id = new Guid("12345678-1234-5678-1234-b00000000000"), Title = "TestBookForUnitTests Part 1", AuthorId = new Guid("12345678-1234-5678-1234-a00000000000")},
            new Book { Id = new Guid("12345678-1234-5678-1234-b00000000001"), Title = "TestBookForUnitTests Part 2", AuthorId = new Guid("12345678-1234-5678-1234-a00000000000")},
            new Book { Id = Guid.NewGuid(), Title = "Carrie", AuthorId = new Guid("12345678-1234-5678-1234-a00000000001")},
            new Book { Id = Guid.NewGuid(), Title = "The Green Mile", AuthorId = new Guid("12345678-1234-5678-1234-a00000000001")},
            new Book { Id = Guid.NewGuid(), Title = "Den gröna milen", AuthorId = new Guid("12345678-1234-5678-1234-a00000000001")},
            new Book { Id = Guid.NewGuid(), Title = "Sagan om Ringen", AuthorId = new Guid("12345678-1234-5678-1234-a00000000002")},
            new Book { Id = Guid.NewGuid(), Title = "The Hobbit", AuthorId = new Guid("12345678-1234-5678-1234-a00000000002")},
            new Book { Id = Guid.NewGuid(), Title = "God natt, Alfons Åberg", AuthorId = new Guid("12345678-1234-5678-1234-a00000000003")},
            new Book { Id = Guid.NewGuid(), Title = "Kalas Alfons Åberg", AuthorId = new Guid("12345678-1234-5678-1234-a00000000003")},
            new Book { Id = Guid.NewGuid(), Title = "Vad sa pappa Åberg?", AuthorId = new Guid("12345678-1234-5678-1234-a00000000003")},
        };
    }
}

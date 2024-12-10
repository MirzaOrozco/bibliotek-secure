using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.MemoryDatabase
{
    static public class CreateTestDb
    {
        static public RealDatabase CreateInMemoryTestDbWithData()
        {
            // Create a new test db on request, add random to the name, otherwise the same memory database is used or something.
            var options = new DbContextOptionsBuilder<RealDatabase>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Random.Shared.Next())
                .Options;
            var db = new RealDatabase(options);

            // Fill with testable data
            db.Authors.AddRange(
                new Author { Id = new Guid("12345678-1234-5678-1234-a00000000000"), Name = "TestAuthorForUnitTests" },
                new Author { Id = new Guid("12345678-1234-5678-1234-a00000000001"), Name = "Steven King" },
                new Author { Id = new Guid("12345678-1234-5678-1234-a00000000002"), Name = "JRR Tolkien" },
                new Author { Id = new Guid("12345678-1234-5678-1234-a00000000003"), Name = "Gunilla Bergström" },
                new Author { Id = new Guid("12345678-1234-5678-1234-a0000000000a"), Name = "AuthorWithoutBooksForUnitTests" }
            );
            db.Books.AddRange(
                new Book { Id = new Guid("12345678-1234-5678-1234-b00000000000"), Title = "TestBookForUnitTests Part 1", AuthorId = new Guid("12345678-1234-5678-1234-a00000000000") },
                new Book { Id = new Guid("12345678-1234-5678-1234-b00000000001"), Title = "TestBookForUnitTests Part 2", AuthorId = new Guid("12345678-1234-5678-1234-a00000000000") },
                new Book { Id = Guid.NewGuid(), Title = "Carrie", AuthorId = new Guid("12345678-1234-5678-1234-a00000000001") },
                new Book { Id = Guid.NewGuid(), Title = "The Green Mile", AuthorId = new Guid("12345678-1234-5678-1234-a00000000001") },
                new Book { Id = Guid.NewGuid(), Title = "Den gröna milen", AuthorId = new Guid("12345678-1234-5678-1234-a00000000001") },
                new Book { Id = Guid.NewGuid(), Title = "Sagan om Ringen", AuthorId = new Guid("12345678-1234-5678-1234-a00000000002") },
                new Book { Id = Guid.NewGuid(), Title = "The Hobbit", AuthorId = new Guid("12345678-1234-5678-1234-a00000000002") },
                new Book { Id = Guid.NewGuid(), Title = "God natt, Alfons Åberg", AuthorId = new Guid("12345678-1234-5678-1234-a00000000003") },
                new Book { Id = Guid.NewGuid(), Title = "Kalas Alfons Åberg", AuthorId = new Guid("12345678-1234-5678-1234-a00000000003") },
                new Book { Id = Guid.NewGuid(), Title = "Vad sa pappa Åberg?", AuthorId = new Guid("12345678-1234-5678-1234-a00000000003") }
            );
            db.SaveChanges();
            return db;
        }
    }
}

using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RealDatabase : DbContext
    {
        public RealDatabase(DbContextOptions options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        */
    }
}

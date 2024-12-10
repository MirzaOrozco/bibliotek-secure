using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Guid? ReservedByUserId { get; set; }
    }
}

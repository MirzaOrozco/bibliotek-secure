using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

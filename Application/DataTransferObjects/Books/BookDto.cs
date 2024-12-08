namespace Application.DataTransferObjects.Books
{
    public class BookDto
    {
        public string Title { get; set; } = String.Empty;
        public Guid AuthorId { get; set; }
    }
}

namespace Application.Models;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int AuthorId { get; set; }

    public string Author { get; set; }
    
    public string Description { get; set; }
}

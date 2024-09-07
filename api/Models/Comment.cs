namespace api.Models;

public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string Content { get; set; } = string.Empty;


    public int? StockId { get; set; }

    // Navigation property - creates cyclic reference
    public Stock? Stock { get; set; }

}
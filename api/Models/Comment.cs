namespace api.Models;

public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime CretedOn { get; set; } = DateTime.Now;

    public int? StockId { get; set; }

    // Navigation property - creates cyclic reference
    public Stock? Stock { get; set; }
}
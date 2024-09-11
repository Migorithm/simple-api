using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Comment")]
public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string Content { get; set; } = string.Empty;


    // Navigation property - creates cyclic reference?
    public int? StockId { get; set; }

    public Stock? Stock { get; set; }

    // One to one is ensured by not having a list
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }


}
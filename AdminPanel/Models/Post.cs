using System.ComponentModel;

namespace AdminPanel.Models;

public class Post : BaseEntity
{


    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime PublishDate { get; set; }
    public ICollection<PopularTagPost>? PostsPopularTag { get; set; }
    [DisplayName("Category")]
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }


}

using System.ComponentModel;

namespace AdminPanel.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    [DisplayName("Parent Category")]
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public virtual ICollection<Category>? SubCategories { get; set; }
    public virtual ICollection<Post>? Posts { get; set; }


}

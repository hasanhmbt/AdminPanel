namespace AdminPanel.Models;

public class PopularTag : BaseEntity
{
    public string TagName { get; set; } = null!;

    public virtual ICollection<PopularTagPost>? PopularTagPosts { get; set; }

}

namespace AdminPanel.Models;

public class PopularTag : BaseEntity
{
    public string TagName { get; set; } = null!;

    public ICollection<PopularTagPost>? PopularTagPosts { get; set; }

}

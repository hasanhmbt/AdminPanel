using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdminPanel.Conficurations;

public class PopularTagPostMapping : IEntityTypeConfiguration<PopularTagPost>
{
    public void Configure(EntityTypeBuilder<PopularTagPost> builder)
    {

        builder.HasKey(pt => new { pt.PostId, pt.PopularTagId });

        builder.HasOne(pt => pt.Post)
            .WithMany(p => p!.PostsPopularTag)
            .HasForeignKey(pt => pt.PostId);

        builder.HasOne(pt => pt.PopularTag)
            .WithMany(p => p!.PopularTagPosts)
            .HasForeignKey(pt => pt.PopularTagId);
    }
}
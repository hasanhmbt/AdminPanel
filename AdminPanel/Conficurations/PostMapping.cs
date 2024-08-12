using AdminPanel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Conficurations;

public class PostMapping : BaseEntityMapping<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);


        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Content)
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c!.Posts)
            .HasForeignKey(p => p.CategoryId);
    }
}

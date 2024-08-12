using AdminPanel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdminPanel.Conficurations;

public class PopularTagMapping : BaseEntityMapping<PopularTag>
{
    public override void Configure(EntityTypeBuilder<PopularTag> builder)
    {
        base.Configure(builder);

        //builder.ToTable("PopulerEtiketler");

        builder.Property(pt => pt.TagName)
            .HasMaxLength(50)
            .IsRequired();
    }
}

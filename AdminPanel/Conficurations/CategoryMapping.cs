using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Conficurations;


public class BaseEntityMapping<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
    }
}

public class CategoryMapping : BaseEntityMapping<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        //builder.ToTable("Kategoriler");

        builder.Property(c => c.Name)
            //.HasColumnName("Baslik")
            .HasMaxLength(100)
            .IsRequired();

        //  Configure the relationship between Post and Category  
        builder.HasMany(c => c.Posts)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);


        //  Configure self-referencing relationship for Category
        builder.HasOne(c => c.ParentCategory)
            .WithMany(c => c!.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViteLoq.Domain.Templates.Entities;

namespace ViteLoq.Infrastructure.Persistence.Configurations.Templates;

public class NutritionItemConfiguration : IEntityTypeConfiguration<NutritionItem>
{
    public void Configure(EntityTypeBuilder<NutritionItem> builder)
    {
        builder.ToTable("NutritionItems");
        builder.HasKey(n => n.Id);
        
        // builder.Property(n => n.Id).ValueGeneratedNever();
        builder.Property(n => n.Name).IsRequired().HasMaxLength(100);
        builder.Property(n => n.Brand).IsRequired().HasMaxLength(100);
        builder.Property(n => n.CreatedDate).IsRequired();
        builder.Property(n => n.UpdatedDate).IsRequired();
    }
}
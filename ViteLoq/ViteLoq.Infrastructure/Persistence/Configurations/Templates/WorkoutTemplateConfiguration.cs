using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViteLoq.Domain.Templates.Entities;

namespace ViteLoq.Infrastructure.Persistence.Configurations.Templates;

public class WorkoutTemplateConfiguration : IEntityTypeConfiguration<WorkoutTemplate>
{
    public void Configure(EntityTypeBuilder<WorkoutTemplate> builder)
    {
        builder.ToTable("WorkoutTemplate");
        builder.HasKey(e => e.Id);
        
        // builder.Property(n => n.Id).ValueGeneratedNever();
        builder.Property(n => n.CreatedDate).IsRequired();
        builder.Property(n => n.UpdatedDate).IsRequired();
    }
}
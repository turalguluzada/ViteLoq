using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViteLoq.Domain.UserEntry.Entities;

namespace ViteLoq.Infrastructure.Persistence.Configurations.UserEntry
{
    public class UserFoodEntryConfiguration : IEntityTypeConfiguration<UserNutritionEntry>
    {
        public void Configure(EntityTypeBuilder<UserNutritionEntry> builder)
        {
            builder.ToTable("UserFoodEntries");
            builder.HasKey(e => e.Id);

            // builder.Property(e => e.QuantityValue).HasColumnType("decimal(8,2)");
            // builder.Property(e => e.Calories).HasColumnType("decimal(8,2)");
            //
            // builder.Property(e => e.DateTime).IsRequired();
            //
            // // index to support time-range queries per user
            // builder.HasIndex(e => new { e.UserId, e.DateTime });
            //
            // // optionally index on FoodId for joins to NutritionItems
            // builder.HasIndex(e => e.FoodId);
            //
            // // performance: include columns in index (SQL Server INCLUDED) — EF Core supports via HasIndex(...).IncludeProperties(...)
            // builder.HasIndex(e => new { e.UserId, e.DateTime })
            //     .HasDatabaseName("IX_UserEntries_UserId_DateTime")
            //     .IsUnique(false)
            //     .IncludeProperties(e => new[] { nameof(UserEntry.Calories), nameof(UserEntry.QuantityValue) });
        }
    }
}
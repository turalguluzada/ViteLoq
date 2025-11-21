using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViteLoq.Domain.Auth.Entities;

namespace ViteLoq.Infrastructure.Persistence.Configurations.Auth;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.TokenHash).IsRequired().HasMaxLength(200);
        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.ExpiresAt);
    }
}
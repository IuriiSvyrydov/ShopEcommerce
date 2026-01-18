

using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
               .IsRequired()
               .HasMaxLength(500);

        builder.HasIndex(x => x.Token)
               .IsUnique();

        builder.Property(x => x.ExpiresOnUtc)
               .IsRequired();

        builder.Property(x => x.UserId)
               .IsRequired();

        builder.Property(x => x.ReplacedByTokenId)
               .IsRequired(false);

        builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<RefreshToken>()
               .WithOne()
               .HasForeignKey<RefreshToken>(x => x.ReplacedByTokenId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

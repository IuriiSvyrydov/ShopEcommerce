using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);
        builder.Property(o=>o.Id)
            .IsRequired()
            .ValueGeneratedNever();
        builder.Property(o => o.EmailAddress).IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.OrderStatus)
            .HasConversion<string>()
            .IsRequired();
    }
}

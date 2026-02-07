

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Payment.Infrastructure.Persistence.Configurations;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Domain.Entities.Payment>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Payment> builder)
    {
       builder.ToTable("payments");
       builder.HasKey(p => p.Id);
       builder.Property(p => p.Id).ValueGeneratedNever()
            .IsRequired();
         builder.HasIndex(p => p.OrderId).IsUnique();
        builder.Property(p => p.Amount).HasPrecision(18, 2).IsRequired();


    }
}

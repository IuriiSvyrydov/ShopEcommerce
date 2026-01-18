

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
       builder.HasKey(o => o.Id);
        builder.Property(x => x.Id).IsRequired()
            .ValueGeneratedNever();
        builder.HasIndex(o => o.CorrelationId);
        builder.Property(o => o.Type).IsRequired().HasMaxLength(250);
        builder.Property(o => o.Content).IsRequired();
        builder.Property(o => o.OccurredOn).IsRequired();
        builder.Property(o => o.ProcessedOn).IsRequired(false);
     
    }
}

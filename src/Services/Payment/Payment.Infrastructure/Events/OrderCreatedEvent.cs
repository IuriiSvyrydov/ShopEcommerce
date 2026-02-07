

namespace Payment.Infrastructure.Events;

public record OrderCreatedEvent
{
    public Guid Id { get; init; }
    public decimal TotalPrice { get; init; }
}

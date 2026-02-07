

namespace Payment.Infrastructure.Events;

public record PaymentCompletedEvent
{
    public Guid OrderId { get; init; }
    public string CorrelationId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
}

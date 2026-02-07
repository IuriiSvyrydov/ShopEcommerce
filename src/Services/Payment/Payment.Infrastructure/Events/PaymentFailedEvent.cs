

namespace Payment.Infrastructure.Events;

public record PaymentFailedEvent
{
    public Guid OrderId { get; init; }
    public string CorrelationId { get; init; }
    public string Reason { get; init; } = "";
}

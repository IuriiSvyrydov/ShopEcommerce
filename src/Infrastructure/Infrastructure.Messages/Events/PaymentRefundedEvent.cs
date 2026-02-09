

namespace Infrastructure.Messages.Events;

public class PaymentRefundedEvent
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "UAH";
    public string Reason { get; init; } = string.Empty;
    public Guid CorrelationId { get; init; }
}

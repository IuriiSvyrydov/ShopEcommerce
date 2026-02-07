

namespace Ordering.Application.Events;

    public record OrderCreatedEvent
    {
        public int OrderId { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; }
        public Guid CorrelationId { get; init; }

}

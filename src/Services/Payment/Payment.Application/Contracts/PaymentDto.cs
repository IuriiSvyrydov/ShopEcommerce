using Payment.Domain.Enums;

namespace Payment.Application.Contracts;

public record PaymentDto
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "UAH";
    public PaymentStatus Status { get; init; }
    public string Provider { get; init; } = "Stripe";
    public string? ProviderPaymentId { get; init; }
    public Guid CorrelationId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime CompletedAt { get; init; }
}

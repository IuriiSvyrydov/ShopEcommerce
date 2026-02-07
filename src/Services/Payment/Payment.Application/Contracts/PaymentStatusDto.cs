
using Payment.Domain.Enums;

namespace Payment.Application.Contracts;

public record PaymentStatusDto
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    public PaymentStatus Status { get; init; } = PaymentStatus.Processing;
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "";
    public string? Reason { get; init; }
}

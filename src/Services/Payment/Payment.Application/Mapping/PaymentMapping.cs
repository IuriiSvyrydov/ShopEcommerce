
using Payment.Domain.Enums;

namespace Payment.Application.Mappings;

public static class PaymentMapping
{
    // Полный DTO для просмотра платежа
    public static PaymentDto ToDto(this Domain.Entities.Payment  payment)
        => new PaymentDto
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Status = payment.Status,
            Provider = payment.Provuder,
            ProviderPaymentId = payment.ProviderPaymentId,
            CorrelationId = payment.CorrelationId,
            CreatedAt = payment.CreatedAt,
            CompletedAt = payment.CompletedAt
        };

    // Только статус платежа
    public static PaymentStatusDto ToStatusDto(this Domain.Entities.Payment payment)
        => new PaymentStatusDto
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Status = payment.Status,
            Reason = payment.Status == PaymentStatus.Failed ? "Payment failed" : null
        };
}



namespace Payment.Application.Events;

public interface IPaymentService
{
    Task AddPaymentAsync(Domain.Entities.Payment payment, CancellationToken ct = default);
    Task UpdatePaymentAsync(Domain.Entities.Payment payment, CancellationToken ct = default);
    Task<PaymentResult> ProcessPaymentAsync(Guid orderId, decimal amount, string currency);
    Task<PaymentStatusDto?> GetPaymentStatusAsync(string orderId, CancellationToken ct = default);


}


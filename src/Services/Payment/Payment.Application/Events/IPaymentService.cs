

namespace Payment.Application.Events;

public interface IPaymentService
{
    Task AddPaymentAsync(Domain.Entities.Payment payment, CancellationToken ct = default);
    Task UpdatePaymentAsync(Domain.Entities.Payment payment, CancellationToken ct = default);
    Task ProcessPaymentAsync(Guid orderId, CancellationToken ct = default);
    Task<PaymentStatusDto?> GetPaymentStatusAsync(string orderId, CancellationToken ct = default);


}


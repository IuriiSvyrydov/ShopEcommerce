

namespace Payment.Application.Interfaces;

public interface IPaymentRepository
{
    Task<Domain.Entities.Payment?>GetByOrderIdAsync(Guid orderId, CancellationToken ct = default);
    Task<Domain.Entities.Payment?> GetByIdAsync(Guid paymentId, CancellationToken ct = default);
    Task AddAsync(Domain.Entities.Payment payment, CancellationToken ct = default);
    Task UpdateAsync(Domain.Entities.Payment payment, CancellationToken ct = default);
}

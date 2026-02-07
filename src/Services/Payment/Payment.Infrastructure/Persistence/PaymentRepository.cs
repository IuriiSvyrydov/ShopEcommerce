

using Payment.Application.Interfaces;

namespace Payment.Infrastructure.Persistence;

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _dbContext;
    public PaymentRepository(PaymentDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Domain.Entities.Payment payment, CancellationToken ct = default)
    {
       _dbContext.Payments.Add(payment);
       await _dbContext.SaveChangesAsync(ct);
    }

    public Task<Domain.Entities.Payment?> GetByOrderIdAsync(Guid orderId, CancellationToken ct = default)
   =>_dbContext.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId, ct);

    public async Task UpdateAsync(Domain.Entities.Payment payment, CancellationToken ct = default)
    {
        _dbContext.Payments.Update(payment);
        await _dbContext.SaveChangesAsync(ct);
    }
}

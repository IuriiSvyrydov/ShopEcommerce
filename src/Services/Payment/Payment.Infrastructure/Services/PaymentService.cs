

using Payment.Application.Events;

namespace Payment.Infrastructure.Services;

public sealed class PaymentService : IPaymentService
{
    public Task<PaymentResult> ProcessPaymentAsync(string orderId, decimal ampount, string currency)
    {
        throw new NotImplementedException();
    }
}

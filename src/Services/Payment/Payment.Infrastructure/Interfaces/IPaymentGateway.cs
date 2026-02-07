

using Payment.Application.Contracts;
using Payment.Application.Events;

namespace Payment.Infrastructure.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentResult> ChargeAsync( string orderId, decimal amount, string currency);
}

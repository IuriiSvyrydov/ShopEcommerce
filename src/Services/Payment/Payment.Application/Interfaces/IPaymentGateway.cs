
namespace Payment.Application.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentResult> ChargeAsync( string orderId, decimal amount, string currency);
    Task RefundAsync(string providerPaymentId, string reason);
}

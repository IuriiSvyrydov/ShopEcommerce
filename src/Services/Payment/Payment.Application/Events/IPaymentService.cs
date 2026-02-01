

namespace Payment.Application.Events;

public interface IPaymentService
{
    Task<PaymentResult>ProcessPaymentAsync(string orderId, decimal ampount, string currency);
}

public record PaymentResult(bool Success, string TransactionId,string? ErrorMessage);
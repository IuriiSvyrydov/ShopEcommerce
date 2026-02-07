
using Payment.Domain.Enums;

namespace Payment.Domain.Entities;

public class Payment
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; } 
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "UAH";
    public PaymentStatus Status { get; private set; } 
    public string Provuder { get; private set; } = "Stripe";
    public string? ProviderPaymentId { get; private set; }
    public Guid CorrelationId { get; private set; }
    public DateTime CreatedAt { get; private set; } 
    public DateTime CompletedAt { get; private set; }
    protected Payment() { }
    public Payment(Guid orderId, decimal amount, string currency, Guid correlationId)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Amount = amount;
        Currency = currency;
        Status = PaymentStatus.Pending;
        CorrelationId = correlationId;
        CreatedAt = DateTime.UtcNow;
    }
    public void MarkProcessing()
    {
        Status = PaymentStatus.Processing;
    }
    public void MarkPaid(string providerPaymentId)
    {
        Status = PaymentStatus.Paid;
        ProviderPaymentId = providerPaymentId;
        CompletedAt = DateTime.UtcNow;
    }
    public void MarkFailed()
    {
        Status = PaymentStatus.Failed;
        CompletedAt = DateTime.UtcNow;
    }
    public void MarkRefunded()
    {
        Status = PaymentStatus.Refunded;
        CompletedAt = DateTime.UtcNow;
    }


}

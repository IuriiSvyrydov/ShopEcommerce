namespace Payment.Infrastructure;

using Stripe;
using Application.Contracts;
using Payment.Application.Interfaces;

public sealed class PaymentGateway : IPaymentGateway
{
    public async Task<PaymentResult> ChargeAsync(string orderId, decimal amount, string currency)
    {
        try
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe ожидает сумму в центах
                Currency = currency.ToLowerInvariant(),
                Metadata = new Dictionary<string, string>
                {
                    ["order_id"] = orderId
                },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                }
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);
            
            return PaymentResult.Ok(intent.Id);
        }
        catch (StripeException ex)
        {
            return PaymentResult.Fail(ex.StripeError?.Message ?? ex.Message);
        }
        catch (Exception ex)
        {
            return PaymentResult.Fail(ex.Message);
        }
    }

    public Task RefundAsync(string providerPaymentId, string reason)
    {
        throw new NotImplementedException();
    }
}


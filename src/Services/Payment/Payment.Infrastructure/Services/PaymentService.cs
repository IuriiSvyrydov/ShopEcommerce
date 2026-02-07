using MassTransit;
using Payment.Application.Contracts;
using Payment.Application.Interfaces;
using Payment.Infrastructure.Events;
using PaymentResult = Payment.Application.Contracts.PaymentResult;

namespace Payment.Infrastructure.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IPaymentGateway _paymentGateway;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPublishEndpoint publishEndpoint, IPaymentGateway paymentGateway, IPaymentRepository paymentRepository)
    {
        _publishEndpoint = publishEndpoint;
        _paymentGateway = paymentGateway;
        _paymentRepository = paymentRepository;

    }

    public async Task<PaymentStatusDto?> GetPaymentStatusAsync(string orderId, CancellationToken ct = default)
    {
       if (!Guid.TryParse(orderId, out var orderGuid))
           throw new ArgumentException("Invalid order ID format", nameof(orderId));
       var payment = await _paymentRepository.GetByOrderIdAsync(orderGuid, ct);
        if (payment == null)
            return null;
        return new PaymentStatusDto
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Status = payment.Status

        };

    }

    public Task AddPaymentAsync(Domain.Entities.Payment payment, CancellationToken ct = default)
    => _paymentRepository.AddAsync(payment, ct);

    public Task UpdatePaymentAsync(Domain.Entities.Payment payment, CancellationToken ct = default)
    => _paymentRepository.UpdateAsync(payment, ct);

    public async  Task<PaymentResult> ProcessPaymentAsync(Guid orderId, decimal amount, string currency)
    {
        var payment = await _paymentRepository.GetByOrderIdAsync(orderId)
                      ?? new Domain.Entities.Payment(orderId, amount, currency, Guid.NewGuid());
        payment.MarkProcessing();
        await _paymentRepository.UpdateAsync(payment);
        PaymentResult result;
        try
        {
            result = await _paymentGateway.ChargeAsync(orderId.ToString(), amount, currency);

        }
        catch (Exception e)
        {
            payment.MarkFailed();
            await _paymentRepository.UpdateAsync(payment);
            await _publishEndpoint.Publish(new PaymentFailedEvent
            {
                OrderId = payment.OrderId,
                CorrelationId = payment.CorrelationId.ToString(),
                Reason = e.Message
            });
            throw;
        }

        if (result.Success)
        {
            payment.MarkPaid(result.TransactionId);
            await _paymentRepository.UpdateAsync(payment);
            await _publishEndpoint.Publish(new PaymentCompletedEvent
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Currency = payment.Currency,
                CorrelationId = payment.CorrelationId.ToString()
            });

        }
        else
        {
            payment.MarkFailed();
            await _paymentRepository.UpdateAsync(payment);
            await _publishEndpoint.Publish(new PaymentFailedEvent
            {
                OrderId = payment.OrderId,
                CorrelationId = payment.CorrelationId.ToString(),
                Reason = result.ErrorMessage
            });
        }
        return result;
    }
}

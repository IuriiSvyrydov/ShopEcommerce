using Infrastructure.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Payment.Infrastructure.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(IPaymentService paymentService, ILogger<OrderCreatedConsumer> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"[Payment] Creating payment for Order Id: {message.Id}, Amount: {message.TotalPrice}");

      
        var payment = new Domain.Entities.Payment(
            orderId: message.OrderId,
            amount: message.TotalPrice,
            currency: "UAH", // можно брать из Order
            correlationId: context.CorrelationId ?? Guid.NewGuid()
        );

        // Сохраняем в БД
        await _paymentService.AddPaymentAsync(payment);

        try
        {
            // Пытаемся обработать платёж через платежный шлюз
            var result = await _paymentService.ProcessPaymentAsync(payment.OrderId, payment.Amount, payment.Currency);

            if (result.Success)
            {
                payment.MarkPaid(result.TransactionId);
                await _paymentService.UpdatePaymentAsync(payment);

                await context.Publish(new PaymentCompletedEvent
                {
                    OrderId = payment.OrderId,
                    CorrelationId = payment.CorrelationId
                });

                _logger.LogInformation($"[Payment] Payment completed for Order Id: {payment.OrderId}");
            }
            else
            {
                payment.MarkFailed();
                await _paymentService.UpdatePaymentAsync(payment);

                await context.Publish(new PaymentFailedEvent
                {
                    OrderId = payment.OrderId,
                    CorrelationId = payment.CorrelationId,
                    Reason = result.ErrorMessage
                });

                _logger.LogWarning($"[Payment] Payment failed for Order Id: {payment.OrderId}, Reason: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            payment.MarkFailed();
            await _paymentService.UpdatePaymentAsync(payment);

            await context.Publish(new PaymentFailedEvent
            {
                OrderId = payment.OrderId,
                CorrelationId = payment.CorrelationId,
                Reason = ex.Message
            });

            _logger.LogError(ex, $"[Payment] Exception processing payment for Order Id: {payment.OrderId}");
        }
    }
}

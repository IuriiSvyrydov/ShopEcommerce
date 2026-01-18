

using Infrastructure.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Payment.Infrastructure.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IPublishEndpoint _endpoint;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(IPublishEndpoint endpoint, ILogger<OrderCreatedConsumer>logger)
    {
        _endpoint = endpoint;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"Processing payment for Order Id {message.Id}");
        await Task.Delay(1000);
        if (message.TotalPrice > 0)
        {
            var completedEvent = new PaymentCompletedEvent
            {
                OrderId = message.Id,
                CorrelationId = context.CorrelationId.Value
            };
            await _endpoint.Publish(completedEvent);
            _logger.LogInformation($"Payment success for order id{message.Id} and CorrelationId: {context.CorrelationId}");
        }
        else
        {
            var failEvent = new PaymentFailedEvent
            {
                OrderId = message.Id,
                CorrelationId = context.CorrelationId.Value,
                Reason = "Total price was zero or negative"

            };
            await _endpoint.Publish(failEvent);
            _logger.LogInformation($"Payment failed for Order Id {message.Id} and CorrelationId: {context.CorrelationId}");

        }
    }
}

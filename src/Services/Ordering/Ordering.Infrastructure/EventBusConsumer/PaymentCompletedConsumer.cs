

using Infrastructure.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.EventBusConsumer;

public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<PaymentCompletedConsumer> _logger;
    public PaymentCompletedConsumer(IOrderRepository orderRepository, ILogger<PaymentCompletedConsumer> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;

    }


    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
        if (order == null)
        {
            _logger.LogInformation($"Order  not found for Id: {context.Message.OrderId}{context.CorrelationId}");
        }
        order.OrderStatus = OrderStatus.Paid;
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation($" Order Id  marked as paid {context.Message.OrderId}");
    }
}

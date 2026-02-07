

namespace Ordering.Infrastructure.EventBusConsumer;

public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<PaymentFailedConsumer> _logger;
    public PaymentFailedConsumer(IOrderRepository orderRepository, ILogger<PaymentFailedConsumer> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
       var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
        if (order == null)
        {
            _logger.LogWarning($"Order with Id {context.Message.OrderId} not found and CorrelationId: {context.CorrelationId}" );
            return;
        }
        order.OrderStatus = OrderStatus.Failed;
        await _orderRepository.UpdateAsync(order);
        _logger.LogWarning($"Payment failed for Order Id  {context.Message.OrderId},{context.Message.Reason}");
    }
}

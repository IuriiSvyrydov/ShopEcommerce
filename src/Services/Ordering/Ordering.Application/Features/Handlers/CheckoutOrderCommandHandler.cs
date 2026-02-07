

namespace Ordering.Application.Features.Handlers;

public sealed class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand,Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;
    public CheckoutOrderCommandHandler(IOrderRepository orderRepository,ILogger<CheckoutOrderCommandHandler>logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = request.ToEntity();
        var generateOrder = await _orderRepository.AddAsync(orderEntity);
        // outbox pattern can be applied here
        var outboxMessage = OrderMapping.ToOrderCreatedOutboxMessage(generateOrder,request.CorrelationId);
        await _orderRepository.AddOutboxMessage(outboxMessage);
        _logger.LogInformation($"Order {generateOrder.Id} is successfully created.and CorrelationId is {request.CorrelationId}");
        return generateOrder.Id;
    }
}





namespace Ordering.Application.Features.Handlers;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;
    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderRepository.GetByIdAsync(request.OrderId);
        if (orderToDelete == null)
        {
            throw new OrderNotFoundException(nameof(Order), request.OrderId);
        }
        await _orderRepository.DeleteAsync(orderToDelete);
        _logger.LogInformation("Order with id: {OrderId} has been successfully deleted.", request.OrderId);
        return Unit.Value;

    }
}

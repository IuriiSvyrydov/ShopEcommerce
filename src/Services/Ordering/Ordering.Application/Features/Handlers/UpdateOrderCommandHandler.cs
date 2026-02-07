

using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Handlers;

public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    public UpdateOrderCommandHandler(IOrderRepository orderRepository,ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
        if (orderToUpdate == null)
        {
            throw new  OrderNotFoundException(nameof(Order), request.Id);

        }
        orderToUpdate.MapUpdateOrder(request);
        await _orderRepository.UpdateAsync(orderToUpdate);
        var outboxMessage = OrderMapping.ToOrderUpdatedOutboxMessage(orderToUpdate, request.CorrelationId);
        await _orderRepository.AddOutboxMessage(outboxMessage);
        _logger.LogInformation("Order with Id: {Id} updated successfully.", request.Id);
        return Unit.Value;
    }

    }

using Ordering.Application.DTO;
using Ordering.Application.Features.Queries;

namespace Ordering.Application.Features.Handlers;

public sealed class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderDTO>>
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderListQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<List<OrderDTO>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByUserName(request.UserName);
        return orders.Select(order => order.ToDto()).ToList();

    }
}

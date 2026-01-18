
using Ordering.Domain.Abstracts;
using Ordering.Domain.Entities;

namespace Ordering.Domain.Repositories;

public interface IOrderRepository: IAsyncRepository<Order>
{
    Task<IReadOnlyList<Order>> GetOrdersByUserName(string userName);
    Task AddOutboxMessage(OutboxMessage outboxMessage);

}

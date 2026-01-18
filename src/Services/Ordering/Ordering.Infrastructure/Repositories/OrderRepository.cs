


namespace Ordering.Infrastructure.Repositories;

public sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    private readonly OrderContext _dbContext;
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOutboxMessage(OutboxMessage outboxMessage)
    {
        await _dbContext.Set<OutboxMessage>().AddAsync(outboxMessage);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<IReadOnlyList<Order>> GetOrdersByUserName(string userName)
    {
        return await _dbContext.Orders
            .Where(o => o.UserName == userName)
            .AsNoTracking()
            .ToListAsync();
    }
}




using Basket.Domain.Entities;
using Domain.Repositories;

namespace Basket.Infrastructure.Repositories;

public sealed class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _distributedCache;
    public BasketRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        
    }
    public async Task DeleteBasket(string userName)
    {
        await _distributedCache.RemoveAsync(userName);

    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _distributedCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
        {
            return null;
        }
        return JsonSerializer.Deserialize<ShoppingCart>(basket);

    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        var serializedBasket = JsonSerializer.Serialize(basket);
        await _distributedCache.SetStringAsync(basket.UserName, serializedBasket);
        return await GetBasket(basket.UserName);

    }
}

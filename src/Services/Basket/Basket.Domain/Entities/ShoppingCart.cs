

namespace Basket.Domain.Entities;

public class ShoppingCart
{
    public Guid Id { get;  set; } = Guid.NewGuid();
    public string UserId { get; set; } = default!;
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new();
    public ShoppingCart()
    {
        
    }

    public ShoppingCart(string userName)
    {
        Id = Guid.NewGuid();
        UserName = userName;
    }
}

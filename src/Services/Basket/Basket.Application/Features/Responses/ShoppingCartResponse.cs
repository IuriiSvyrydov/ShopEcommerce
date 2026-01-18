

namespace Basket.Application.Features.Responses;

public record class ShoppingCartResponse
{
    public string UserName { get; set; }
    public List<ShoppingCartItemResponse> Items { get; init; } = new();
    public ShoppingCartResponse()
    {
        UserName = string.Empty;
        Items = new List<ShoppingCartItemResponse>();
    }
    public ShoppingCartResponse(string userName):this(userName, new List<ShoppingCartItemResponse>())
    {
    
   
    }
    public ShoppingCartResponse(string userName, List<ShoppingCartItemResponse> items)
    {
        UserName = userName;
        Items = items;
    }
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
   
}



namespace Basket.Application.Features.Responses;

public record class ShoppingCartItemResponse
{
    public string ProductId {  get; set; }
   public string ProductName {  get; set; }
   public int Quantity {  get; set; }
    public decimal Price {  get; set; }
    public string ImageFile {  get; set; }
}


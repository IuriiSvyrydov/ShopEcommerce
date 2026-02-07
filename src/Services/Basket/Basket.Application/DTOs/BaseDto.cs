

namespace Basket.Application.DTOs;

public record ShoppingCartDto(
    string UserName,
    List<ShoppingCartItem> Items,
    decimal TotalPrice);

public record ShoppingCartItemDto(
    string ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    string ImageFile);
    public record BasketCheckoutDto(
        string UserName,
        decimal TotalPrice,
        string FirstName,
        string LastName,
        string EmailAddress,
        string AddressLine,
        string Country,
        string State,
        string ZipCode,
     
        int PaymentMethod,
        string Currency);
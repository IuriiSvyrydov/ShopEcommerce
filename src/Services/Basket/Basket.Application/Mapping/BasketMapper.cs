
namespace Basket.Application.Mapping;

public static class BasketMapper
{
    public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart) =>
        new ShoppingCartResponse
        {
            UserName = shoppingCart.UserName,
            Items = shoppingCart.Items.Select(item => new ShoppingCartItemResponse
            {
                Quantity = item.Quantity,
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName
            }).ToList(),

        };

    public static ShoppingCart ToEntity(this CreateShoppingCartCommand command) =>
        new()
        {
            UserName = command.UserName,
            Items = command.Items.Select(item => new ShoppingCartItem
            {
                Quantity = item.Quantity,
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName
            }).ToList(),
        };
    public static ShoppingCart ToEntity(this ShoppingCartResponse response) =>
        new()
        {
            UserName = response.UserName,
            Items = response.Items.Select(item => new ShoppingCartItem
            {
                Quantity = item.Quantity,
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName
            }).ToList(),
        };
    public static BasketCheckoutEvent ToBasketCheckoutEvent(this BasketCheckoutDto dto, ShoppingCart basket) =>
        new()
        {
            UserName = dto.UserName,
            TotalPrice = dto.TotalPrice,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            AddressLine = dto.AddressLine,
            Country = dto.Country,
            State = dto.State,
            ZipCode = dto.ZipCode,
            CardName = dto.CardName,
            CardNumber = dto.CardNumber,
            Expiration = dto.Expiration,
            Cvv = dto.Cvv,
            PaymentMethod = dto.PaymentMethod
        };
}

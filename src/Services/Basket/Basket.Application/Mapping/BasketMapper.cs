namespace Basket.Application.Mapping;

public static class BasketMapper
{
    public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart) =>
        new()
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
            Id = Guid.NewGuid(),
            UserId = response.UserName, // ⚠️ временно, лучше потом брать из токена
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


    public static BasketCheckoutEvent ToBasketCheckoutEvent(
        this BasketCheckoutDto dto,
        ShoppingCart basket,
        Guid correlationId) =>
        new()
        {
            BasketId = basket.Id,
            UserId = basket.UserName, // или отдельный UserId если есть

            TotalPrice = dto.TotalPrice,
            Currency = dto.Currency,

            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,

            AddressLine = dto.AddressLine,
            Country = dto.Country,
            State = dto.State,
            ZipCode = dto.ZipCode,

            PaymentMethod = dto.PaymentMethod,

            CorrelationId = correlationId
        };
}

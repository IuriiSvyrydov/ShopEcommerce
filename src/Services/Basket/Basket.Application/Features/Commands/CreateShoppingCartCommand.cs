using Application.DTOs;

namespace Basket.Application.Features.Commands;

public record CreateShoppingCartCommand(string UserName,List<CreateShoppingCartItemDto> Items)
    : IRequest<ShoppingCartResponse>
{
    public static CreateShoppingCartCommand FromUser(
        string userId,
        CreateShoppingCartCommand incomming)=>
        new (userId, incomming.Items);
}

namespace Basket.Application.Features.Commands;

public record DeleteBasketByUserNameCommand(string UserName) : IRequest<Unit>;
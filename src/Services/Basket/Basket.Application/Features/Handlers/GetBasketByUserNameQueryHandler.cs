
using Application.Features.Queries;
using Basket.Application.Features.Responses;
using Basket.Application.Mapping;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Handlers;

public sealed class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    public GetBasketByUserNameQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basketRepository.GetBasket(request.UserName);
        if (shoppingCart == null)
        {
            return new ShoppingCartResponse(request.UserName)
            {

                Items = new List<ShoppingCartItemResponse>()
            };
        }
        return shoppingCart.ToResponse();
    }
}

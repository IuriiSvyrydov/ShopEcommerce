using Basket.Application.Interfaces;
using Basket.Application.Mapping;
using Domain.Repositories;


namespace Application.Features.Handlers;

public sealed class CreateShoppingCartCommandHandler: IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IDiscountGrpcService _discountGrpcService;
    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository,IDiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        // Communicate with Discount.Grpc and calculate latest prices of products into shopping cart
        foreach (var item in request.Items)
        {
            var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);
            if (coupon != null)
            {
                item.Price -= coupon.Amount;
            }
        }
        var shoppingCart = request.ToEntity();
        var updateCart = await _basketRepository.UpdateBasket(shoppingCart);
        return updateCart.ToResponse();

    }
}
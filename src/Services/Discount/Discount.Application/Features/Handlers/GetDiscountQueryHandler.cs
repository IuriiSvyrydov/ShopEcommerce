using Discount.Application.Mapping;
using Discount.Domain.Repositories;
using Grpc.Core;

namespace Discount.Application.Features.Handlers;

public sealed class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponDto>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountQueryHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            var validationErrors = new Dictionary<string, string>
            {
                { "ProductName", "ProductName cannot be null or empty." }
            };
            // Возможно, здесь нужно выбросить исключение или вернуть ошибку, если ProductName некорректен
        }
        var coupon = await _discountRepository.GetDiscountAsync(request.ProductName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }
        return coupon.ToDto();
    }
}



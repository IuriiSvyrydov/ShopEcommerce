
using Application.Features.Commands;

using Discount.Application.Helpers;
using Discount.Application.Mapping;
using Discount.Domain.Repositories;
using Grpc.Core;

namespace Discount.Application.Features.Handlers;

public sealed class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponDto>
{
    private readonly IDiscountRepository _discountRepository;
    public CreateDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<CouponDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var validationError = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            validationError.Add(nameof(request.ProductName), "ProductName is required.");
        }
        if(string.IsNullOrWhiteSpace(request.Description))
            validationError.Add(nameof(request.Description), "Description is required.");
        if(request.Amount <= 0)
            validationError.Add(nameof(request.Amount), "Amount must be greater than zero.");
        if(validationError.Any())
            throw GrpcErrorHelper.CreateValidationException(validationError);
        var coupon = request.ToEntity();
        var createdCoupon = await _discountRepository.CreateDiscountAsync(coupon);
        if (!createdCoupon)
        {
            throw new RpcException(new Status(StatusCode.Internal, $"Failed to create discount.{request.ProductName}"));
        }
        return coupon.ToDto();
    }
}


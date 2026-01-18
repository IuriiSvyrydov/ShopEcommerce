
using Discount.Application.Helpers;
using Discount.Application.Mapping;
using Discount.Domain.Repositories;
using Grpc.Core;

namespace Discount.Application.Features.Handlers;

public sealed class UpdateDiscountCommandHandler: IRequestHandler<UpdateDiscountCommand, CouponDto>
{
    private readonly Discount.Domain.Repositories.IDiscountRepository _discountRepository;
    public UpdateDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
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
        var updated = await _discountRepository.UpdateDiscountAsync(coupon);
        if (!updated)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount updated failed {request.ProductName}"));
        }
        return coupon.ToDto();


    }
}
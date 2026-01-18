namespace Discount.Application.Features.Commands;

public record UpdateDiscountCommand(int Id, string ProductName, string Description, int Amount)
    : IRequest<CouponDto>;

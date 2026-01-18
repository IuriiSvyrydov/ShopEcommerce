using Application.Features.Commands;
using Discount.Domain.Entities;

namespace Discount.Application.Mapping;

public static class CouponMapper
{
    public static CouponDto ToDto(this Coupon coupon) =>
        new CouponDto(
            coupon.Id,
            coupon.ProductName,
            coupon.Description,
            coupon.Amount
        );

    public static Coupon ToEntity(this CouponDto dto) =>
        new Coupon
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            Description = dto.Description,
            Amount = dto.Amount
        };
    public static Coupon ToEntity(this CreateDiscountCommand command) =>
          new Coupon
          {
             // Id = command.Id,
              ProductName = command.ProductName,
              Description = command.Description,
              Amount = command.Amount
          };
    public static Coupon ToEntity(this UpdateDiscountCommand command) =>
        new Coupon {
            Id = command.Id,
            ProductName = command.ProductName,
            Description = command.Description,
            Amount = command.Amount
        };
}

using Application.Features.Commands;
using Discount.API.Grpc.Protos;
using Discount.Application.DTOs;
using Discount.Domain.Entities;

namespace Discount.API.Mapping;

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
    public static CouponModel ToModel(this CouponDto dto) =>
        new CouponModel
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            Description = dto.Description,
            Amount = dto.Amount
        };

    public static CreateDiscountCommand ToCreateCommand(this CouponModel model) =>
        new CreateDiscountCommand(
            model.ProductName,
            model.Description,
            model.Amount
        );

    public static UpdateDiscountCommand ToUpdateCommand(this CouponModel model) =>
        new UpdateDiscountCommand(
            model.Id,
            model.ProductName,
            model.Description,
            model.Amount
        );
}

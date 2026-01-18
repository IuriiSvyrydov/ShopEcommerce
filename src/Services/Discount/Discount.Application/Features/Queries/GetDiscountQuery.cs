

using Discount.Application.DTOs;
using MediatR;

namespace Application.Features.Queries;

public record GetDiscountQuery(string ProductName): IRequest<CouponDto>;


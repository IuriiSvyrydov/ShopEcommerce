

using Discount.Application.DTOs;
using MediatR;

namespace Application.Features.Commands;

public record CreateDiscountCommand(string ProductName, string Description, int Amount) : IRequest<CouponDto>;

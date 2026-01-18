namespace Discount.Application.Features.Commands;

public record DeleteDiscountCommand(string ProductName): IRequest<bool>;

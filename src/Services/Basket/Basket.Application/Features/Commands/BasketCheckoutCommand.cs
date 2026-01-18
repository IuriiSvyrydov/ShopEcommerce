
namespace Basket.Application.Features.Commands;

public record BasketCheckoutCommand(BasketCheckoutDto Dto) : IRequest<Unit>;

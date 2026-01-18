

using Basket.Application.Mapping;

namespace Basket.Application.Features.Handlers;

public class BasketCheckoutCommandHandler : IRequestHandler<BasketCheckoutCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<BasketCheckoutCommandHandler> _logger;
    public BasketCheckoutCommandHandler(IMediator mediator, IPublishEndpoint publishEndpoint,ILogger<BasketCheckoutCommandHandler> logger)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        
    }
    public async Task<Unit> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var basketResponse = await _mediator.Send(new GetBasketByUserNameQuery(dto.UserName));
        if (!basketResponse.Items.Any())
        {
            throw new InvalidOperationException($"Basket is empty for user {dto.UserName}");
        }
        var basket = basketResponse.ToEntity();
        var eventMessage = dto.ToBasketCheckoutEvent(basket);
        _logger.LogInformation("Publishing BasketCheckoutEvent for User: {UserName}, TotalPrice: {TotalPrice}", eventMessage.UserName, eventMessage.TotalPrice);
        await _publishEndpoint.Publish(eventMessage, cancellationToken);
        await _mediator.Send(new DeleteBasketByUserNameCommand(dto.UserName), cancellationToken);
        return Unit.Value;

    }
    
}
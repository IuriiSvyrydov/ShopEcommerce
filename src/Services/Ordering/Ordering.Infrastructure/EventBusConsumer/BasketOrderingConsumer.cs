
namespace Ordering.Infrastructure.EventBusConsumer;

public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<BasketOrderingConsumer> _logger;

    public BasketOrderingConsumer(
        IMediator mediator,
        ILogger<BasketOrderingConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        using var scope = _logger.BeginScope(
            "Consume BasketOrderingConsumer Event. CorrelationId: {CorrelationId}",
            context.CorrelationId);

        var command = context.Message.ToCheckoutOrderCommand();

        await _mediator.Send(command, context.CancellationToken);

        _logger.LogInformation(
            "BasketOrderingConsumer Event consumed successfully. CorrelationId: {CorrelationId}",
            context.CorrelationId);
    }
}

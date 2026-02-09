using Infrastructure.Messages.Events;
using MassTransit;
using Payment.Application.Features.Commands;

namespace Payment.Infrastructure.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        await context.Send<ProcessPaymentCommand>(new
        {
            OrderId = context.Message.OrderId
        });
    }
}

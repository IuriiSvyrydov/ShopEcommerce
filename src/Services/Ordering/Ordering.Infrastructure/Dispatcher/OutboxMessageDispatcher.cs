using Infrastructure.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ordering.Infrastructure.Dispatcher;

public class OutboxMessageDispatcher : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxMessageDispatcher> _logger;


public OutboxMessageDispatcher(IServiceProvider serviceProvider, ILogger<OutboxMessageDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();

            var dbcontext = scope.ServiceProvider.GetRequiredService<OrderContext>();
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            var pendingMessages = await dbcontext.OutboxMessages
                .Where(x => x.ProcessedOn == null)
                .OrderBy(x => x.OccurredOn)
                .Take(20)
                .ToListAsync(stoppingToken);

            foreach (var message in pendingMessages)
            {
                try
                {
                    var orderCreatedEvent = JsonConvert.DeserializeObject<OrderCreatedEvent>(message.Content)!;

                    await publishEndpoint.Publish(orderCreatedEvent, stoppingToken);

                    message.ProcessedOn = DateTime.UtcNow;

                    _logger.LogInformation("Published outbox message {MessageId}", message.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to publish outbox message {MessageId}", message.Id);
                }
            }

            await dbcontext.SaveChangesAsync(stoppingToken);

            try
            {
                await Task.Delay(5000, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Outbox dispatcher stopping.");
                break; // корректно выходим из цикла
            }
        }
    }


}

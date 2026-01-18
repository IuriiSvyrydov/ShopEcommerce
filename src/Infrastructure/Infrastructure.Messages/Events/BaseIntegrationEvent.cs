
namespace Infrastructure.Messages.Events;

public class BaseIntegrationEvent
{
    public int Id { get; set; }

    public Guid CorrelationId { get; set; }
    public DateTime CreationDate { get; set; }

    public BaseIntegrationEvent()
    {
        CorrelationId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }
    public BaseIntegrationEvent(Guid correlationId, DateTime creationDate)
    {
        CorrelationId = correlationId;
        CreationDate = creationDate;
    }

}

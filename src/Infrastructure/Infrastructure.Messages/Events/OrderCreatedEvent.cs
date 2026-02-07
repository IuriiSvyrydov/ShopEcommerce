namespace Infrastructure.Messages.Events;

public class OrderCreatedEvent : BaseIntegrationEvent
{
    public Guid OrderId { get; init; }
    public decimal TotalPrice { get; init; }
    public string Currency { get; init; } = "UAH";
    public int PaymentMethod { get; set; }

}
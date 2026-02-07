namespace Infrastructure.Messages.Events;

public class BasketCheckoutEvent : BaseIntegrationEvent
{
    public Guid BasketId { get; init; }
    public string UserId { get; init; } = default!;

    public decimal TotalPrice { get; init; }
    public string Currency { get; init; } = "UAH";

    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string EmailAddress { get; init; } = default!;

    public string AddressLine { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string State { get; init; } = default!;
    public string ZipCode { get; init; } = default!;

    public int PaymentMethod { get; init; }

    public Guid CorrelationId { get; init; }
}
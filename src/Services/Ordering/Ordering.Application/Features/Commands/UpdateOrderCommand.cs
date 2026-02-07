using Ordering.Domain.Enums;

namespace Ordering.Application.Features.Commands;

public record UpdateOrderCommand : IRequest<Unit>
{
    public Guid Id { get; init; }

    // Customer
    public string UserName { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string EmailAddress { get; init; } = default!;

    // Shipping
    public string AddressLine { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string State { get; init; } = default!;
    public string ZipCode { get; init; } = default!;

    // Order
    public decimal TotalPrice { get; init; }
    public int PaymentMethod { get; init; }

    // Optional lifecycle updates
    public OrderStatus? OrderStatus { get; init; }
    public PaymentStatus? PaymentStatus { get; init; }

    public Guid CorrelationId { get; set; }
}
using Ordering.Domain.Abstracts;
using Ordering.Domain.Enums;

namespace Ordering.Domain.Entities;

public class Order : BaseEntity
{
    public string UserName { get; set; } = default!;


    // Customer
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public decimal TotalPrice { get; set; }
    public string EmailAddress { get; set; } = default!;

    // Shipping
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;

    // Payment (SAFE)
    public int PaymentMethod { get; set; }
    public string Currency { get; set; } = "UAH";
    public string? PaymentIntentId { get; set; }   // Stripe / PayPal / etc
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    // Order lifecycle
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
}
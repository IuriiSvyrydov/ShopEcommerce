

namespace Ordering.Application.DTO;

public record CreateOrderDto(
    string UserName,
    decimal TotalPrice,
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode, 
    int PaymentMethod,
    string Currency,
    string? CardName);


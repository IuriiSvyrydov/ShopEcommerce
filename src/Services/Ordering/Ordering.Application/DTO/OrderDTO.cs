
namespace Ordering.Application.DTO;

public record OrderDTO(
    Guid Id,
    string UserName,
    string FirstName,
    string LastName,
    decimal TotalPrice,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode,
    int PaymentMethod,
    string Currency,
    OrderStatus OrderStatus
    );


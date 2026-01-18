
namespace Identity.Application.DTOs;

public record UserDTO(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    IEnumerable<string> Roles
);



using Identity.Application.DTOs;

namespace Identity.Application.Services;

public interface IUserService
{
    Task<UserDTO?> GetUserByIdAsync(Guid userId);
}

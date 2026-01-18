
using Identity.Application.DTOs;

namespace Identity.Application.Interfaces;

public interface IUserRepository
{
    Task<UserDTO?> GetByEmailAsync(string email);
    Task<string?> GeneratePasswordResetTokenAsync(Guid id);
    Task<bool>ResetPasswordAsync(Guid userId, string token, string newPassword);
}

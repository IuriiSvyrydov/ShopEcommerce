using Identity.Application.DTOs;
using Identity.Application.Interfaces;

namespace Identity.Infrastructure.Interfaces;

public sealed class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string?> GeneratePasswordResetTokenAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return null;
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<UserDTO?> GetByEmailAsync(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        if (appUser == null)
            return null;

        var roles = await _userManager.GetRolesAsync(appUser);

        return new UserDTO(
            appUser.Id,
            appUser.FirstName,
            appUser.LastName,
            appUser.Email!,
            roles
        );
    }

    public async Task<bool> ResetPasswordAsync(Guid userId, string token, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return false;
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}

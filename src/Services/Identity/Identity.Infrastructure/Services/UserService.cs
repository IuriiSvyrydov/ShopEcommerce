using Identity.Application.DTOs;

namespace Identity.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) 
            throw new Exception("User not found");
        var roles = await _userManager.GetRolesAsync(user);
        return new UserDTO(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email!,
            roles
        );

    }
}

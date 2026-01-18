
using Identity.Application.Exceptions;
using Identity.Application.Exceptions.Identity;
using Identity.Application.Features.Commands;

namespace Identity.Infrastructure.Services;

internal class IdentityService : IIdentityService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    public IdentityService(UserManager<ApplicationUser> userManager,ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw IdentityException.InvalidCredentials();

        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
        if (!isValidPassword)
            throw IdentityException.InvalidCredentials();

        var roles = await _userManager.GetRolesAsync(user);

        var token = _tokenService.GenerateJwtToken(
            user.Id,
            user.Email!,
            user.FirstName!,   
            user.LastName!,    
            roles
        );

        return new LoginResult(token, DateTime.UtcNow.AddMinutes(30));
    }


    public async Task RegisterAsync(string firstname, string lastname, string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstname,
            LastName = lastname,

        };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e =>
                new IdentityErrorItem(e.Code, e.Description));

            throw new IdentityException(errors);
        }


         
    }
}

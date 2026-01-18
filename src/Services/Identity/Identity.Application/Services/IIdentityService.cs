

using Identity.Application.Features.Commands;

namespace Identity.Application.Services;

public interface IIdentityService
{
    Task<LoginResult> LoginAsync(string email, string password);
    Task RegisterAsync( string firstname, string lastname, string email,string password);
}

using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Exceptions.Identity;

public static class IdentityErrorMapper
{
    public static IEnumerable<IdentityErrorItem> Map(
        IEnumerable<IdentityError> errors)
    {
        return errors.Select(e =>
            new IdentityErrorItem(e.Code,e.Description));
    }
}
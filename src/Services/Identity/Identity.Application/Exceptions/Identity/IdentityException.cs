using Identity.Application.Exceptions.Identity;

namespace Identity.Application.Exceptions;

public  class IdentityException : Exception
{
    public IReadOnlyCollection<IdentityErrorItem> Errors { get; }

    public IdentityException(IEnumerable<IdentityErrorItem> errors)
        : base("Identity operation failed")
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public static IdentityException InvalidCredentials()
        => new IdentityException(new[]
        {
            new IdentityErrorItem(
                "InvalidCredentials",
                "Invalid credentials")
        });
}
namespace Identity.Application.Exceptions.Users;

public sealed class UserNotFoundException : ApplicationException
{
    public IReadOnlyCollection<UserNotFoundItem> Errors { get; }
    public UserNotFoundException(IEnumerable<UserNotFoundItem> errors)
        : base("User not found")
    {
        Errors = errors.ToList().AsReadOnly();
    }
    public static UserNotFoundException WithId(string userId)
        => new UserNotFoundException(new[]
        {
            new UserNotFoundItem(
                "UserNotFound",
                $"User with ID '{userId}' was not found.")
        });
}

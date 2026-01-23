

namespace Currency.Domain.Exceptions;

public class DomainException: Exception
{
    public IReadOnlyCollection<CurrencyErrorItem> Errors { get; }
    public DomainException(IEnumerable<CurrencyErrorItem> errors)
        : base("Currency domain operation failed")
    {
        Errors = errors.ToList().AsReadOnly();
    }
    public static DomainException InvalidCurrencyCode()
        => new DomainException(new[]
        {
            new CurrencyErrorItem(
                "InvalidCurrencyCode",
                "The provided currency code is invalid.")
        });
}

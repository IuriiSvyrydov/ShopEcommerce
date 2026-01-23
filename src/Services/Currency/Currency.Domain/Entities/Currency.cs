

using Currency.Domain.Exceptions;

namespace Currency.Domain.Entities;

public sealed class Currency : IEquatable<Currency>
{
    public string Code { get;  }
    private Currency(string code)
    {
        Code = code;
    }
    public static Currency USD => new Currency("USD");
    public static Currency EUR => new Currency("EUR");
    public static Currency UAH => new Currency("UAH");

    public static Currency From(string code)
    {
        if(string.IsNullOrWhiteSpace(code))
            throw new DomainException("Currency code cannot be null or empty.");
    public bool Equals(Currency? other)
    {
        throw new NotImplementedException();
    }
}

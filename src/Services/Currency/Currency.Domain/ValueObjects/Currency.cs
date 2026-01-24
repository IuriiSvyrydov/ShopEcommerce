using Currency.Domain.Exceptions;

namespace Currency.Domain.ValueObjects;

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
        if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
            throw DomainException.InvalidCurrencyCode();

        code = code.ToUpperInvariant();

        return code switch
        {
            "USD" => USD,
            "EUR" => EUR,
            "UAH" => UAH,
            _ => new Currency(code)
        };
    }

    public bool Equals(Currency? other)
        => other is not null && Code == other.Code;
    public override bool Equals(object? obj)
        => obj is Currency other && Equals(other);
    public override int GetHashCode()
        => Code.GetHashCode();
    public override string ToString()
        => Code;
}

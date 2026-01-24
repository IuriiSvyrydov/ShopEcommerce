using Currency.Domain.Exceptions;

namespace Currency.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; }
    
    
    
    public Currency Currency { get; }
    private Money(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw DomainException.InvalidCurrencyCode();
        Amount = decimal.Round(amount, 2);
        Currency = currency;
    }
    public  Money Convert(decimal rate, Currency currency)
    {
        if (rate <= 0)
            throw DomainException.InvalidCurrencyCode();
        return new Money(Amount*rate, currency);
    }
    public override  string ToString()
        => $"{Amount} {Currency}";

    public bool Equals(Money? other)
        => other is not null && Amount == other.Amount &&
           Currency.Equals(other.Currency);

    public override bool Equals(object? obj)
        => Equals(obj as Money);
    

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

   

}

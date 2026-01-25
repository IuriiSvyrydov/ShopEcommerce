namespace Currency.API.Contracts
{
    public record ConvertRequest(decimal Amount, string FromCurrency, string ToCurrency);
    public record ConvertResponse(decimal Amount, string Currency);
    public record RateResponse(string BaseCurrency, string TargetCurrency,decimal Rate, DateTimeOffset UpdatedAtUtc);

}

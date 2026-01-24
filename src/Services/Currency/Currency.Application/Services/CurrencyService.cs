

using Currency.Application.Interfaces;
using Currency.Domain;
using Currency.Domain.Entities;

using Currency.Domain.Interfaces;
using Currency.Domain.ValueObjects;


namespace Currency.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
 
        private readonly ICurrencyRateProvider _rateProvider;
        private readonly ICurrencyRateRepository _currencyRateRepository;
        public CurrencyService(ICurrencyRateProvider rateProvider, ICurrencyRateRepository currencyRateRepository)
        {
            _rateProvider = rateProvider;
            _currencyRateRepository = currencyRateRepository;
        }
        public async Task<Money> ConvertAsync(Money money, Domain.ValueObjects.Currency targetCurrency, CancellationToken ct = default)
        {
            if (money.Currency.Equals(targetCurrency)) return money;
            
            var rate = await GetRateAsync(money.Currency, targetCurrency, ct);
            return money.Convert(rate.Rate, targetCurrency);

        }

        public async Task<CurrencyRate> GetRateAsync(Domain.ValueObjects.Currency baseCurrency, Domain.ValueObjects.Currency targetCurrency, CancellationToken ct = default)
        {
            var rate = await _currencyRateRepository.GetAsync(baseCurrency, targetCurrency, ct);
            if (rate is not null && !rate.IsExpired)
                return rate;
            var freshRateValue = await _rateProvider.GetRateAsync(baseCurrency, targetCurrency, ct);
            var freshRate = new CurrencyRate(baseCurrency, targetCurrency, freshRateValue, DateTime.UtcNow);
            //Save
            var rateEntity = CurrencyRate.Create(baseCurrency, targetCurrency, freshRateValue, TimeSpan.FromHours(1));
            await _currencyRateRepository.SaveAsync(rateEntity, ct);
            return rateEntity;
        }

    }
}

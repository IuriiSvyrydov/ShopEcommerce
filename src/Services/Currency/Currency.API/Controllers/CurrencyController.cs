using Currency.API.Contracts;
using Currency.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Currency.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        [HttpPost("convert")]
        public async Task<ActionResult<ConvertResponse>> Convert([FromBody] ConvertRequest request,CancellationToken ctx)
        {
            var money = new Domain.ValueObjects.Money(request.Amount, Domain.ValueObjects.Currency.From(request.FromCurrency));
            var convertedMoney = await _currencyService.ConvertAsync(money, Domain.ValueObjects.Currency.From(request.ToCurrency), ctx);
            return Ok(new ConvertResponse(
                   convertedMoney.Amount,
                   convertedMoney.Currency.Code
            ));

        }
        /// <summary>
        /// Получить текущий курс (из кэша/БД)
        /// </summary>
        [HttpGet("rate")]
        public async Task<ActionResult<RateResponse>> GetRate(
            [FromQuery] string baseCurrency,
            [FromQuery] string targetCurrency,
            CancellationToken ct)
        {
            var rate = await _currencyService.GetRateAsync(
                Domain.ValueObjects.Currency.From(baseCurrency),
                Domain.ValueObjects.Currency.From(targetCurrency),
                ct);

            if (rate is null)
                return NotFound();

            return Ok(new RateResponse(
                rate.BaseCurrency.Code,
                rate.TargetCurrency.Code,
                rate.Rate,
                rate.UpdatedAtUtc));
        }

        /// <summary>
        /// Принудительно обновить курс (admin / cron)
        /// </summary>
        [HttpPost("rate/refresh")]
        public async Task<ActionResult<RateResponse>> RefreshRate(
            [FromQuery] string baseCurrency,
            [FromQuery] string targetCurrency,
            CancellationToken ct)
        {
            var rate = await _currencyService.RefreshRateAsync(
               Domain.ValueObjects.Currency.From(baseCurrency),
               Domain.ValueObjects.Currency.From(targetCurrency),
                ct);

            return Ok(new RateResponse(
                rate.BaseCurrency.Code,
                rate.TargetCurrency.Code,
                rate.Rate,
                rate.UpdatedAtUtc));
        }

    }
}

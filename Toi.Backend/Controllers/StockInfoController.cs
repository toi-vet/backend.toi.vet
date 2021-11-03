using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Toi.Backend.Models;
using Toi.Backend.Services.ExchangeRateService;
using Toi.Backend.Services.StockPriceService;

namespace Toi.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockInfoController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeService;
        private readonly IStockPriceService _priceService;
        private readonly IMemoryCache _cache;

        public StockInfoController(IExchangeRateService exchangeService, IStockPriceService priceService, IMemoryCache cache)
        {
            _exchangeService = exchangeService;
            _priceService = priceService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<StockInfo>> GetStockInfo([FromQuery] string symbol, [FromQuery] string toCurrency)
            => await _cache.GetOrCreateAsync<ActionResult<StockInfo>>((symbol, toCurrency), async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                var stockPrice = await _priceService.GetCurrentPriceAsync(symbol);
                if (stockPrice is null)
                {
                    return NotFound($"No stock price information found for symbol {symbol}");
                }

                var exchangeRate = await _exchangeService.GetExchangeRateAsync(stockPrice.Currency, toCurrency);
                if (exchangeRate is null)
                {
                    return NotFound($"No exchange rate found for currency pair {stockPrice.Currency} {toCurrency}");
                }

                var intradayData = await _priceService.GetIntradayDataAsync(symbol);
                if (intradayData is null)
                {
                    return NotFound($"No intraday data found for symbol {symbol}");
                }

                var info = new StockInfo
                {
                    Symbol = stockPrice.Symbol,
                    ExchangeRate = exchangeRate, 
                    StockPrice = stockPrice, 
                    IntradayData = intradayData
                };
                info.ConvertCurrencies();
                return info;
            });
    }
}
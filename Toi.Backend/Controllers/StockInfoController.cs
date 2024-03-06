using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<StockInfo>> GetStockInfo()
        {
            Currency toCurrency = "EUR";
            var item = await _cache.GetOrCreateAsync<ActionResult<StockInfo>>(toCurrency, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                var stockPrice = await _priceService.GetCurrentPriceAsync();
                if (stockPrice is null)
                {
                    return NotFound($"No stock price information found");
                }

                var exchangeRate = await _exchangeService.GetExchangeRateAsync();
                if (exchangeRate is null)
                {
                    return NotFound($"No exchange rate found for currency pair {stockPrice.Currency} {toCurrency}");
                }

                var intradayData = await _priceService.GetIntradayDataAsync();
                if (intradayData is null)
                {
                    return NotFound($"No intraday data found");
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
            return item ?? NotFound();
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toi.Backend.Models;

namespace Toi.Backend.Services.ExchangeRateService
{
    public class YahooFinanceExchangeRateService(ILogger<YahooFinanceExchangeRateService> logger, HttpClient client) : IExchangeRateService
    {
        private readonly ILogger<YahooFinanceExchangeRateService> _logger = logger;
        private readonly HttpClient _client = client;

        public async Task<ExchangeRate?> GetExchangeRateAsync()
        {
            var url = $"https://query1.finance.yahoo.com/v8/finance/quote?symbols={"CAD"}{"EUR"}%3DX";
            try
            {
                var response = await _client.GetFromJsonAsync<YahooFinanceQuoteResponse>(url);
                return response?.ToExchangeRate("CAD", "EUR");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get exchange rate for CAD to EUR");
                return null;
            }
        }
    }
}
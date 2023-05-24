using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Toi.Backend.Models;

namespace Toi.Backend.Services.ExchangeRateService
{
    public class YahooFinanceExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _client;

        public YahooFinanceExchangeRateService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ExchangeRate?> GetExchangeRateAsync()
        {
            var url = $"https://query1.finance.yahoo.com/v8/finance/quote?symbols={"CAD"}{"EUR"}%3DX";
            var response = await _client.GetFromJsonAsync<YahooFinanceQuoteResponse>(url);
            return response?.ToExchangeRate("CAD", "EUR");
        }
    }
}
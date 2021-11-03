using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Toi.Backend.Models;

namespace Toi.Backend.Services.StockPriceService
{
    public class YahooFinanceStockPriceService : IStockPriceService
    {
        private readonly HttpClient _client;

        public YahooFinanceStockPriceService(HttpClient client)
        {
            _client = client;
        }

        public async Task<StockPrice?> GetCurrentPriceAsync(string symbol)
        {
            var url = $"https://query1.finance.yahoo.com/v7/finance/quote?symbols={symbol}";
            var response = await _client.GetFromJsonAsync<YahooFinanceQuoteResponse>(url);
            return response?.ToStockPrice();
        }

        public async Task<List<IntradayDataPoint>?> GetIntradayDataAsync(string symbol)
        {
            var url = $"https://query1.finance.yahoo.com/v8/finance/chart/{symbol}?symbol={symbol}&interval=1m&includePrePost=true";
            var response = await _client.GetFromJsonAsync<YahooFinanceChartResponse>(url);
            return response?.ToIntradayData();
        }
    }
}
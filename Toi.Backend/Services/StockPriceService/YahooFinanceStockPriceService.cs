using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toi.Backend.Models;

namespace Toi.Backend.Services.StockPriceService
{
    public class YahooFinanceStockPriceService(ILogger<YahooFinanceStockPriceService> logger, HttpClient client) : IStockPriceService
    {
        private readonly ILogger<YahooFinanceStockPriceService> _logger = logger;
        private readonly HttpClient _client = client;

        public async Task<StockPrice?> GetCurrentPriceAsync()
        {
            var symbol = "TOI.V";
            var url = $"https://query1.finance.yahoo.com/v6/finance/quote?symbols={symbol}";
            try
            {
                var response = await _client.GetFromJsonAsync<YahooFinanceQuoteResponse>(url);
                return response?.ToStockPrice();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get stock price for {symbol}", symbol);
                return null;
            }
        }

        public async Task<List<IntradayDataPoint>?> GetIntradayDataAsync()
        {
            var symbol = "TOI.V";
            var url = $"https://query1.finance.yahoo.com/v8/finance/chart/{symbol}?symbol={symbol}&interval=1m&includePrePost=true";
            try
            {
                var response = await _client.GetFromJsonAsync<YahooFinanceChartResponse>(url);
                return response?.ToIntradayData();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get intraday data for symbol {symbol}", symbol);
                return null;
            }
        }
    }
}
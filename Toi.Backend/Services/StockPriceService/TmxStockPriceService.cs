using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.Extensions.Logging;
using Toi.Backend.Extensions;
using Toi.Backend.Models;

namespace Toi.Backend.Services.StockPriceService;

public class TmxStockPriceService(ILogger<TmxStockPriceService> logger, GraphQLHttpClient graphqlClient, HttpClient httpClient) : IStockPriceService
{
    private readonly ILogger<TmxStockPriceService> _logger = logger;
    private readonly GraphQLHttpClient _graphqlClient = graphqlClient;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<StockPrice?> GetCurrentPriceAsync()
    {
        var symbol = "TOI:CA";
        var request = new GraphQLRequest
        {
            OperationName = "getQuoteBySymbol",
            Query = @"query getQuoteBySymbol($symbol: String, $locale: String) {
  getQuoteBySymbol(symbol: $symbol, locale: $locale) {
    symbol
    price
    priceChange
    percentChange
    exchangeName
    marketPlace
    volume
    openPrice
    dayHigh
    dayLow
    prevClose
    close
  }
}
",
            Variables = new
            {
                locale = "en",
                symbol,
            }
        };
        var result = await _graphqlClient.SendQueryAsync<TmxQuoteResponse>(request);

        if (result.Errors?.Any() == true || result?.Data?.GetQuoteBySymbol == null)
        {
            return null;
        }

        var quote = result.Data.GetQuoteBySymbol;
        return quote.ToStockPrice();
    }

    public async Task<List<IntradayDataPoint>?> GetIntradayDataAsync()
    {
        var symbol = "TOI.V";
        var url =
            $"https://query1.finance.yahoo.com/v8/finance/chart/{symbol}?symbol={symbol}&interval=1m&includePrePost=true";
        _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
        try
        {
            var response = await _httpClient.GetFromJsonAsync<YahooFinanceChartResponse>(url);
            return response?.ToIntradayData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch intraday data for symbol {Symbol}", symbol);
            return [];
        }
    }
}
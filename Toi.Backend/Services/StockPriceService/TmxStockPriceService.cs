using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using Toi.Backend.Extensions;
using Toi.Backend.Models;

namespace Toi.Backend.Services.StockPriceService;

public class TmxStockPriceService : IStockPriceService
{
    private readonly GraphQLHttpClient _graphqlClient;
    private readonly HttpClient _httpClient;

    public TmxStockPriceService(GraphQLHttpClient graphqlClient, HttpClient httpClient)
    {
        _graphqlClient = graphqlClient;
        _httpClient = httpClient;
    }

    public async Task<StockPrice?> GetCurrentPriceAsync()
    {
        var symbol = "TOI";
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
        var response = await _httpClient.GetFromJsonAsync<YahooFinanceChartResponse>(url);
        return response?.ToIntradayData();
    }
}
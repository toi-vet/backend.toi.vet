namespace Toi.Backend.Models;

using System.Text.Json.Serialization;

public class GetQuoteBySymbol
{
    [JsonPropertyName("symbol")] public string Symbol { get; set; }

    [JsonPropertyName("price")] public decimal? Price { get; set; }

    [JsonPropertyName("priceChange")] public decimal? PriceChange { get; set; }

    [JsonPropertyName("percentChange")] public decimal? PercentChange { get; set; }

    [JsonPropertyName("exchangeName")] public string ExchangeName { get; set; }

    [JsonPropertyName("marketPlace")] public string MarketPlace { get; set; }

    [JsonPropertyName("volume")] public int? Volume { get; set; }

    [JsonPropertyName("openPrice")] public decimal? OpenPrice { get; set; }

    [JsonPropertyName("dayHigh")] public decimal? DayHigh { get; set; }

    [JsonPropertyName("dayLow")] public decimal? DayLow { get; set; }

    [JsonPropertyName("prevClose")] public decimal? PrevClose { get; set; }

    [JsonPropertyName("close")] public decimal? Close { get; set; }
}

public class TmxQuoteResponse
{
    [JsonPropertyName("getQuoteBySymbol")] public GetQuoteBySymbol GetQuoteBySymbol { get; set; }
}
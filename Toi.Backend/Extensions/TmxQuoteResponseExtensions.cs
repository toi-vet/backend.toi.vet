using Toi.Backend.Models;

namespace Toi.Backend.Extensions;

public static class TmxQuoteResponseExtensions
{
    public static StockPrice ToStockPrice(this GetQuoteBySymbol quote)
    {
        return new StockPrice
        {
            Symbol = quote.Symbol,
            Price = quote.Price,
            PriceChange = quote.PriceChange,
            PercentageChange = quote.PercentChange,
            Volume = quote.Volume,
            DayHigh = quote.DayHigh,
            DayLow = quote.DayLow,
            PreviousClosePrice = quote.PrevClose,
            OpenPrice = quote.OpenPrice,
            ClosePrice = quote.Close,
            ExchangeName = quote.ExchangeName,
            Market = "",
            MarketState = quote.OpenPrice == null ? "closed" : "open",
            Currency = "CAD",
        };
    }
}
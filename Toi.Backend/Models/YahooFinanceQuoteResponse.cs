using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Toi.Backend.Models
{
    public class Result
    {
        public string Language { get; init; } = null!;
        public string Region { get; init; } = null!;
        public string QuoteType { get; init; } = null!;
        public string QuoteSourceName { get; init; } = null!;
        public bool Triggerable { get; init; }
        public string Currency { get; init; } = null!;
        public string ShortName { get; init; } = null!;
        public string MarketState { get; init; } = null!;
        public long FirstTradeDateMilliseconds { get; init; }
        public int PriceHint { get; init; }
        public decimal RegularMarketChange { get; init; }
        public decimal RegularMarketChangePercent { get; init; }
        public int RegularMarketTime { get; init; }
        public decimal RegularMarketPrice { get; init; }
        public decimal RegularMarketDayHigh { get; init; }
        public string RegularMarketDayRange { get; init; } = null!;
        public decimal RegularMarketDayLow { get; init; }
        public int RegularMarketVolume { get; init; }
        public decimal RegularMarketPreviousClose { get; init; }
        public decimal Bid { get; init; }
        public decimal Ask { get; init; }
        public int BidSize { get; init; }
        public int AskSize { get; init; }
        public string FullExchangeName { get; init; } = null!;
        public decimal RegularMarketOpen { get; init; }
        public int AverageDailyVolume3Month { get; init; }
        public int AverageDailyVolume10Day { get; init; }
        public decimal FiftyTwoWeekLowChange { get; init; }
        public decimal FiftyTwoWeekLowChangePercent { get; init; }
        public string FiftyTwoWeekRange { get; init; } = null!;
        public decimal FiftyTwoWeekHighChange { get; init; }
        public decimal FiftyTwoWeekHighChangePercent { get; init; }
        public decimal FiftyTwoWeekLow { get; init; }
        public decimal FiftyTwoWeekHigh { get; init; }
        public decimal FiftyDayAverage { get; init; }
        public decimal FiftyDayAverageChange { get; init; }
        public decimal FiftyDayAverageChangePercent { get; init; }
        public decimal TwoHundredDayAverage { get; init; }
        public decimal TwoHundredDayAverageChange { get; init; }
        public decimal TwoHundredDayAverageChangePercent { get; init; }
        public int SourceInterval { get; init; }
        public int ExchangeDataDelayedBy { get; init; }
        public bool Tradeable { get; init; }
        public string Exchange { get; init; } = null!;
        public string MessageBoardId { get; init; } = null!;
        public string ExchangeTimezoneName { get; init; } = null!;
        public string ExchangeTimezoneShortName { get; init; } = null!;
        public int GmtOffSetMilliseconds { get; init; }
        public string Market { get; init; } = null!;
        public bool EsgPopulated { get; init; }
        public string Symbol { get; init; } = null!;

        public StockPrice ToStockPrice()
        {
            return new StockPrice
            {
                Symbol = Symbol,
                Price = RegularMarketPrice,
                PriceChange = RegularMarketChange,
                PercentageChange = RegularMarketChangePercent,
                Volume = RegularMarketVolume,
                DayHigh = RegularMarketDayHigh,
                DayLow = RegularMarketDayLow,
                PreviousClosePrice = RegularMarketPreviousClose,
                OpenPrice = RegularMarketOpen,
                ExchangeName = FullExchangeName,
                Market = Market,
                MarketState = MarketState,
                Currency = Currency
            };
        }
    }

    public class QuoteResponse
    {
        public List<Result> Result { get; init; } = new();
        public object? Error { get; init; }
    }

    public class YahooFinanceQuoteResponse
    {
        public QuoteResponse? QuoteResponse { get; init; }

        public ExchangeRate? ToExchangeRate(Currency from, Currency to)
        {
            return QuoteResponse?.Result.Count < 1 ? null : new ExchangeRate(from, to, QuoteResponse!.Result[0].RegularMarketPrice);
        }

        public StockPrice? ToStockPrice()
        {
            return QuoteResponse?.Result.Count < 1
                ? null
                : QuoteResponse!.Result[0].ToStockPrice();
        }
    }
}
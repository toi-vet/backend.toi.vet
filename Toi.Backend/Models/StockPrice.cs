using System;
using System.Text.Json.Serialization;

namespace Toi.Backend.Models
{
    public class StockPrice
    {
        [JsonIgnore]
        public string Symbol { get; init; }
        public decimal Price { get; init; }
        public decimal PriceConverted { get; private set; }
        public decimal PriceChange { get; init; }
        public decimal PriceChangeConverted { get; private set; }
        public decimal PercentageChange { get; init; }
        public int Volume { get; init; }
        public decimal DayHigh { get; init; }
        public decimal DayHighConverted { get; private set; }
        public decimal DayLow { get; init; }
        public decimal DayLowConverted { get; private set; }
        public decimal PreviousClosePrice { get; init; }
        public decimal PreviousClosePriceConverted { get; private set; }
        public decimal OpenPrice { get; init; }
        public decimal OpenPriceConverted { get; private set; }
        public string ExchangeName { get; init; } = null!;
        public string Market { get; init; } = null!;
        public string MarketState { get; init; } = null!;
        public Currency Currency { get; init; }
        public Currency ConvertedCurrency { get; private set; }

        public void ConvertCurrencies(ExchangeRate exchangeRate)
        {
            var (currency, _, value) = exchangeRate;
            if (Currency.Symbol != currency.Symbol)
            {
                throw new InvalidOperationException($"Currency {Currency.Symbol} does not match {currency.Symbol}");
            }
            PriceConverted = Price * value;
            PriceChangeConverted = PriceChange * value;
            DayHighConverted = DayHigh * value;
            DayLowConverted = DayLow * value;
            PreviousClosePriceConverted = PreviousClosePrice * value;
            OpenPriceConverted = OpenPrice * value;
            ConvertedCurrency = currency;
        }
    }
}
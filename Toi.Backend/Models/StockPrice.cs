using System;
using System.Text.Json.Serialization;

namespace Toi.Backend.Models
{
    public class StockPrice
    {
        [JsonIgnore]
        public string Symbol { get; init; }
        public decimal? Price { get; init; }
        public decimal? PriceConverted { get; private set; }
        public decimal? PriceChange { get; init; }
        public decimal? PriceChangeConverted { get; private set; }
        public decimal? PercentageChange { get; init; }
        public int? Volume { get; init; }
        public decimal? DayHigh { get; init; }
        public decimal? DayHighConverted { get; private set; }
        public decimal? DayLow { get; init; }
        public decimal? DayLowConverted { get; private set; }
        public decimal? PreviousClosePrice { get; init; }
        
        public decimal? ClosePrice { get; init; }
        public decimal? ClosePriceConvered { get; private set; }
        public decimal? PreviousClosePriceConverted { get; private set; }
        public decimal? OpenPrice { get; init; }
        public decimal? OpenPriceConverted { get; private set; }
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

            if (Price.HasValue)
            {
                PriceConverted = Price.Value * value;    
            }

            if (PriceChange.HasValue)
            {
                PriceChangeConverted = PriceChange.Value * value;    
            }

            if (DayHigh.HasValue)
            {
                DayHighConverted = DayHigh.Value * value;    
            }

            if (DayLow.HasValue)
            {
                DayLowConverted = DayLow.Value * value;    
            }
            
            if (PreviousClosePrice.HasValue)
            {
                PreviousClosePriceConverted = PreviousClosePrice.Value * value;    
            }
            
            OpenPriceConverted = OpenPrice * value;
            if (ClosePrice.HasValue)
            {
                ClosePriceConvered = ClosePrice.Value * value;    
            }
            ConvertedCurrency = "EUR";
        }
    }
}
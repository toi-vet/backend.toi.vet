using System.Collections.Generic;

namespace Toi.Backend.Models
{
    public readonly struct StockInfo
    {
        public string Symbol { get; init; }
        public StockPrice StockPrice { get; init; }
        public ExchangeRate ExchangeRate { get; init; }
        public List<IntradayDataPoint> IntradayData { get; init; }

        public void ConvertCurrencies()
        {
            StockPrice.ConvertCurrencies(ExchangeRate);
            foreach (var datapoint in IntradayData)
            {
                datapoint.ConvertCurrencies(ExchangeRate);
            }
        }
    }
}
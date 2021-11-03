using System.Collections.Generic;

namespace Toi.Backend.Models
{
    public record IntradayDataPoint(int Timestamp, OpenHighLowVolume? Ohlv)
    {
        public void ConvertCurrencies(ExchangeRate exchangeRate)
        {
            if (Ohlv is null)
            {
                return;
            }
            
            var (_, _, value) = exchangeRate;
            Ohlv.HighConverted = Ohlv.High * value;
            Ohlv.LowConverted = Ohlv.Low * value;
            Ohlv.OpenConverted = Ohlv.Open * value;
            Ohlv.ConvertedCurrency = exchangeRate.To;
        }
    }
}
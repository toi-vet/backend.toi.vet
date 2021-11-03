namespace Toi.Backend.Models
{
    public class OpenHighLowVolume
    {
        public OpenHighLowVolume(decimal open, decimal high, decimal low, int volume, Currency? currency)
        {
            Open = open;
            High = high;
            Low = low;
            Volume = volume;
            Currency = currency;
        }

        public decimal Open { get; }
        public decimal OpenConverted { get; set; }
        public decimal High { get; }
        public decimal HighConverted { get; set; }
        public decimal Low { get; }
        public decimal LowConverted { get; set; }
        public int Volume { get; }
        
        public Currency? Currency { get; }
        public Currency ConvertedCurrency { get; set; }

    }
}
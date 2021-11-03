using System.Collections.Generic;
using System.Linq;

namespace Toi.Backend.Models
{
    public class Pre
    {
        public string? Timezone { get; set; } = null!;
        public int Start { get; set; }
        public int End { get; set; }
        public int Gmtoffset { get; set; }
    }

    public class Regular
    {
        public string Timezone { get; set; } = null!;
        public int Start { get; set; }
        public int End { get; set; }
        public int Gmtoffset { get; set; }
    }

    public class Post
    {
        public string Timezone { get; set; } = null!;
        public int Start { get; set; }
        public int End { get; set; }
        public int Gmtoffset { get; set; }
    }

    public class CurrentTradingPeriod
    {
        public Pre? Pre { get; set; }
        public Regular? Regular { get; set; }
        public Post? Post { get; set; }
    }

    public class Meta
    {
        public string? Currency { get; set; }
        public string? Symbol { get; set; }
        public string? ExchangeName { get; set; }
        public string? InstrumentType { get; set; }
        public int? FirstTradeDate { get; set; }
        public int? RegularMarketTime { get; set; }
        public int? Gmtoffset { get; set; }
        public string? Timezone { get; set; }
        public string? ExchangeTimezoneName { get; set; }
        public decimal? RegularMarketPrice { get; set; }
        public decimal? ChartPreviousClose { get; set; }
        public decimal? PreviousClose { get; set; }
        public int? Scale { get; set; }
        public int? PriceHint { get; set; }
        public CurrentTradingPeriod? CurrentTradingPeriod { get; set; }
        public string? DataGranularity { get; set; }
        public string? Range { get; set; }
        public List<string?> ValidRanges { get; set; } = new();
    }

    public class Quote
    {
        public List<int?> Volume { get; set; } = new();
        public List<decimal?> Close { get; set; } = new();
        public List<decimal?> Open { get; set; } = new();
        public List<decimal?> Low { get; set; } = new();
        public List<decimal?> High { get; set; } = new();
    }

    public class Indicators
    {
        public List<Quote> Quote { get; set; } = new();
    }

    public class ChartResult
    {
        public Meta? Meta { get; set; }
        public List<int> Timestamp { get; set; } = new();
        public Indicators? Indicators { get; set; }
    }

    public class Chart
    {
        public List<ChartResult> Result { get; set; } = new();
        public object? Error { get; set; }
    }

    public class YahooFinanceChartResponse
    {
        public Chart? Chart { get; set; }

        public List<IntradayDataPoint>? ToIntradayData()
        {
            var data = Chart?.Result.FirstOrDefault();
            if (data is null)
            {
                return null;
            }

            var datapoints = new List<IntradayDataPoint>();
            for (var i = 0; i < data.Timestamp.Count; i++)
            {
                var timestamp = data.Timestamp[i];
                var quote = data.Indicators?.Quote[0];
                var open = quote?.Open[i];
                var high = quote?.High[i];
                var low = quote?.Low[i];
                var volume = quote?.Volume[i];
                var currency = data.Meta?.Currency;
                if (!open.HasValue || !high.HasValue || !low.HasValue || !volume.HasValue)
                {
                    datapoints.Add(new IntradayDataPoint(timestamp, null));
                }
                else
                {
                    datapoints.Add(new IntradayDataPoint(timestamp, new OpenHighLowVolume(open.Value, high.Value, low.Value, volume.Value, currency)));
                }
            }
            return datapoints;
        }
    }
}
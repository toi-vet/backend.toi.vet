namespace Toi.Backend.Models;

using System.Text.Json.Serialization;


public class GetQuoteBySymbol
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("priceChange")]
        public decimal? PriceChange { get; set; }

        [JsonPropertyName("percentChange")]
        public decimal? PercentChange { get; set; }

        [JsonPropertyName("exchangeName")]
        public string ExchangeName { get; set; }

        [JsonPropertyName("exShortName")]
        public string ExShortName { get; set; }

        [JsonPropertyName("exchangeCode")]
        public string ExchangeCode { get; set; }

        [JsonPropertyName("marketPlace")]
        public object MarketPlace { get; set; }

        [JsonPropertyName("sector")]
        public string Sector { get; set; }

        [JsonPropertyName("industry")]
        public string Industry { get; set; }

        [JsonPropertyName("volume")]
        public int? Volume { get; set; }

        [JsonPropertyName("openPrice")]
        public decimal? OpenPrice { get; set; }

        [JsonPropertyName("dayHigh")]
        public decimal? DayHigh { get; set; }

        [JsonPropertyName("dayLow")]
        public decimal? DayLow { get; set; }

        [JsonPropertyName("MarketCap")]
        public long? MarketCap { get; set; }

        [JsonPropertyName("MarketCapAllClasses")]
        public long? MarketCapAllClasses { get; set; }

        [JsonPropertyName("peRatio")]
        public string PeRatio { get; set; }

        [JsonPropertyName("prevClose")]
        public decimal? PrevClose { get; set; }

        [JsonPropertyName("dividendFrequency")]
        public object DividendFrequency { get; set; }

        [JsonPropertyName("dividendYield")]
        public object DividendYield { get; set; }

        [JsonPropertyName("dividendAmount")]
        public object DividendAmount { get; set; }

        [JsonPropertyName("dividendCurrency")]
        public object DividendCurrency { get; set; }

        [JsonPropertyName("beta")]
        public decimal? Beta { get; set; }

        [JsonPropertyName("eps")]
        public decimal? Eps { get; set; }

        [JsonPropertyName("exDividendDate")]
        public string ExDividendDate { get; set; }

        [JsonPropertyName("longDescription")]
        public string LongDescription { get; set; }

        [JsonPropertyName("fulldescription")]
        public string Fulldescription { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("fullAddress")]
        public string FullAddress { get; set; }

        [JsonPropertyName("employees")]
        public string Employees { get; set; }

        [JsonPropertyName("shareOutStanding")]
        public int? ShareOutStanding { get; set; }

        [JsonPropertyName("totalDebtToEquity")]
        public string TotalDebtToEquity { get; set; }

        [JsonPropertyName("totalSharesOutStanding")]
        public int? TotalSharesOutStanding { get; set; }

        [JsonPropertyName("sharesESCROW")]
        public int? SharesESCROW { get; set; }

        [JsonPropertyName("vwap")]
        public decimal? Vwap { get; set; }

        [JsonPropertyName("dividendPayDate")]
        public object DividendPayDate { get; set; }

        [JsonPropertyName("weeks52high")]
        public decimal? Weeks52high { get; set; }

        [JsonPropertyName("weeks52low")]
        public decimal? Weeks52low { get; set; }

        [JsonPropertyName("alpha")]
        public decimal? Alpha { get; set; }

        [JsonPropertyName("averageVolume10D")]
        public int? AverageVolume10D { get; set; }

        [JsonPropertyName("averageVolume30D")]
        public int? AverageVolume30D { get; set; }

        [JsonPropertyName("averageVolume50D")]
        public int? AverageVolume50D { get; set; }

        [JsonPropertyName("priceToBook")]
        public decimal? PriceToBook { get; set; }

        [JsonPropertyName("priceToCashFlow")]
        public string PriceToCashFlow { get; set; }

        [JsonPropertyName("returnOnEquity")]
        public string ReturnOnEquity { get; set; }

        [JsonPropertyName("returnOnAssets")]
        public string ReturnOnAssets { get; set; }

        [JsonPropertyName("day21MovingAvg")]
        public decimal? Day21MovingAvg { get; set; }

        [JsonPropertyName("day50MovingAvg")]
        public decimal? Day50MovingAvg { get; set; }

        [JsonPropertyName("day200MovingAvg")]
        public decimal? Day200MovingAvg { get; set; }

        [JsonPropertyName("dividend3Years")]
        public string Dividend3Years { get; set; }

        [JsonPropertyName("dividend5Years")]
        public int? Dividend5Years { get; set; }

        [JsonPropertyName("datatype")]
        public string Datatype { get; set; }

        [JsonPropertyName("issueType")]
        public string IssueType { get; set; }

        [JsonPropertyName("close")]
        public decimal? Close { get; set; }

        [JsonPropertyName("__typename")]
        public string Typename { get; set; }
    }

    public class TmxQuoteResponse
    {
        [JsonPropertyName("getQuoteBySymbol")]
        public GetQuoteBySymbol GetQuoteBySymbol { get; set; }
    }


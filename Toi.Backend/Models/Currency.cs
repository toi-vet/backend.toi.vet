using System.Text.Json.Serialization;
using Toi.Backend.Converters;

namespace Toi.Backend.Models
{
    [JsonConverter(typeof(CurrencyConverter))]
    public readonly struct Currency
    {
        public Currency(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }

        public static implicit operator string(Currency c) => c.Symbol;
        public static implicit operator Currency(string c) => new(c);

        public override string ToString()
        {
            return Symbol;
        }
    }
}
namespace Toi.Backend.Models
{
    public record ExchangeRate(Currency From, Currency To, decimal Value);
}
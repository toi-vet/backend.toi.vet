using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Toi.Backend.Models;

namespace Toi.Backend.Services.ExchangeRateService;

public class EcbExchangeRateService : IExchangeRateService
{
    private readonly HttpClient _client;

    public EcbExchangeRateService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ExchangeRate?> GetExchangeRateAsync()
    {
        var xml = await _client.GetStringAsync("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
        var serializer = new XmlSerializer(typeof(Envelope));
        using var reader = new StringReader(xml);
        var envelope = (Envelope?)serializer.Deserialize(reader);
        var curr = envelope?.Cube?.Cubes?.FirstOrDefault()?.Cubes?.SingleOrDefault(c => c.Currency == "CAD");
        if (curr != null && decimal.TryParse(curr.Rate, out var exchangeRate))
        {
            return new ExchangeRate("CAD", "EUR", 1 / exchangeRate);
        }
        return null;
    }
}
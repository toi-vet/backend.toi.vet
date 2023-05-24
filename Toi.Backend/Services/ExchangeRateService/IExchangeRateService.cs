using System.Threading.Tasks;
using Toi.Backend.Models;

namespace Toi.Backend.Services.ExchangeRateService
{
    public interface IExchangeRateService
    {
        Task<ExchangeRate?> GetExchangeRateAsync();
    }
}
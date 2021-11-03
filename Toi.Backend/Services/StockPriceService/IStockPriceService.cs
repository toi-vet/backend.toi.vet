using System.Collections.Generic;
using System.Threading.Tasks;
using Toi.Backend.Models;

namespace Toi.Backend.Services.StockPriceService
{
    public interface IStockPriceService
    {
        Task<StockPrice?> GetCurrentPriceAsync(string symbol);
        Task<List<IntradayDataPoint>?> GetIntradayDataAsync(string symbol);
    }
}
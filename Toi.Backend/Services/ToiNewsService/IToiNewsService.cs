using System.Collections.Generic;
using Toi.Backend.Models;

namespace Toi.Backend.Services.ToiNewsService;

public interface IToiNewsService
{
    IList<NewsItem> GetNewsItems(int take);
}
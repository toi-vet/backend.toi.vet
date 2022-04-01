using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Toi.Backend.Models;

namespace Toi.Backend.Services.ToiNewsService;

public class ToiNewsService : IToiNewsService
{
    public IList<NewsItem> GetNewsItems(int take)
    {
        var url = "https://topicus.com/rss";
        using var reader = XmlReader.Create(url);
        var feed = SyndicationFeed.Load(reader);
        return feed.Items
            .OrderByDescending(i => i.PublishDate)
            .Take(take)
            .Select(i => new NewsItem(i))
            .ToList();
    }
}
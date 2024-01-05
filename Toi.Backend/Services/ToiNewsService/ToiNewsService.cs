using System;
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
        const string url = "https://www.globenewswire.com/rssfeed/organization/ARDakRGkVrpjF7tpA0plFA==";
        try
        {
            using var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            return feed.Items
                .OrderByDescending(i => i.PublishDate)
                .Take(take)
                .Select(i => new NewsItem(i))
                .ToList();
        }
        catch
        {
            return new List<NewsItem>();
        }
    }
}
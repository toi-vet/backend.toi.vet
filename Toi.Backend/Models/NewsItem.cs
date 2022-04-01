using System;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Toi.Backend.Models;

public class NewsItem
{
    public NewsItem(SyndicationItem item)
    {
        Title = item.Title.Text;
        PublishDate = item.PublishDate;
        Link = item.Links.FirstOrDefault()?.Uri;
    }
    public string Title { get; }
    public Uri? Link { get; }
    public DateTimeOffset PublishDate { get; }

}
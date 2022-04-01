using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Toi.Backend.Models;
using Toi.Backend.Services.ToiNewsService;

namespace Toi.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ToiNewsController : ControllerBase
{
    private readonly IToiNewsService _toiNewsService;
    private readonly IMemoryCache _cache;

    public ToiNewsController(IToiNewsService toiNewsService, IMemoryCache cache)
    {
        _toiNewsService = toiNewsService;
        _cache = cache;
    }

    [HttpGet]
    public ActionResult<IList<NewsItem>> GetNews([FromQuery] int take = 5)
        => _cache.GetOrCreate<ActionResult<IList<NewsItem>>>($"toi-news-{take}", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return Ok(_toiNewsService.GetNewsItems(take));
        });
}
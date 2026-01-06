using Microsoft.AspNetCore.Mvc;
using AUYeni.Services;

namespace AUYeni.Controllers;

public class SitemapController : Controller
{
    private readonly ISitemapService _sitemapService;

    public SitemapController(ISitemapService sitemapService)
    {
        _sitemapService = sitemapService;
    }

    [Route("sitemap.xml")]
    public async Task<IActionResult> Index()
    {
        var xml = await _sitemapService.GenerateSitemapXmlAsync();
        return Content(xml, "application/xml");
    }
}


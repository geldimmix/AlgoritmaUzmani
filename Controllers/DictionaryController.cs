using Microsoft.AspNetCore.Mvc;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers;

public class DictionaryController : Controller
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly ISeoService _seoService;
    private readonly IConfiguration _configuration;

    public DictionaryController(IDictionaryRepository dictionaryRepository, ISeoService seoService, IConfiguration configuration)
    {
        _dictionaryRepository = dictionaryRepository;
        _seoService = seoService;
        _configuration = configuration;
    }

    [Route("dictionary/{namespaceSlug}/{slug?}")]
    public async Task<IActionResult> Index(string namespaceSlug, string? slug)
    {
        var ns = await _dictionaryRepository.GetNamespaceBySlugAsync(namespaceSlug);
        if (ns == null)
        {
            return NotFound();
        }

        var entries = await _dictionaryRepository.GetEntriesByNamespaceAsync(ns.Id);
        ViewBag.Namespace = ns;
        ViewBag.Entries = entries;

        if (!string.IsNullOrEmpty(slug))
        {
            var entry = await _dictionaryRepository.GetEntryBySlugAsync(slug, ns.Id);
            if (entry != null)
            {
                var baseUrl = _configuration["SiteSettings:BaseUrl"];
                var seoMetadata = await _seoService.GenerateSeoMetadataAsync(entry, baseUrl!);
                ViewBag.SeoMetadata = seoMetadata;
                ViewBag.JsonLd = _seoService.GenerateJsonLd(entry, "DefinedTerm");
                ViewBag.CurrentEntry = entry;
            }
        }

        return View();
    }
}


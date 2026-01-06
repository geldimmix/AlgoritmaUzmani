using Microsoft.AspNetCore.Mvc;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers;

public class TutorialController : Controller
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly ISeoService _seoService;
    private readonly IConfiguration _configuration;

    public TutorialController(ITutorialRepository tutorialRepository, ISeoService seoService, IConfiguration configuration)
    {
        _tutorialRepository = tutorialRepository;
        _seoService = seoService;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var tutorials = await _tutorialRepository.GetAllActiveAsync();
        return View(tutorials);
    }

    [Route("tutorial/{slug}/{section?}")]
    public async Task<IActionResult> Detail(string slug, string? section)
    {
        var tutorial = await _tutorialRepository.GetBySlugAsync(slug);
        if (tutorial == null)
        {
            return NotFound();
        }

        var sections = await _tutorialRepository.GetSectionsByTutorialAsync(tutorial.Id);
        ViewBag.Sections = sections;

        var baseUrl = _configuration["SiteSettings:BaseUrl"];
        var seoMetadata = await _seoService.GenerateSeoMetadataAsync(tutorial, baseUrl!);
        ViewBag.SeoMetadata = seoMetadata;
        ViewBag.JsonLd = _seoService.GenerateJsonLd(tutorial, "TechArticle");

        return View(tutorial);
    }
}


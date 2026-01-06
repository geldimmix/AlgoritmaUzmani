using Microsoft.AspNetCore.Mvc;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers;

public class BlogController : Controller
{
    private readonly IBlogRepository _blogRepository;
    private readonly ISeoService _seoService;
    private readonly IConfiguration _configuration;

    public BlogController(IBlogRepository blogRepository, ISeoService seoService, IConfiguration configuration)
    {
        _blogRepository = blogRepository;
        _seoService = seoService;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var posts = await _blogRepository.GetAllActiveAsync();
        return View(posts);
    }

    [Route("blog/{slug}")]
    public async Task<IActionResult> Detail(string slug)
    {
        var post = await _blogRepository.GetBySlugAsync(slug);
        if (post == null)
        {
            return NotFound();
        }

        // Increment view count
        await _blogRepository.IncrementViewCountAsync(post.Id);

        // Generate SEO metadata
        var baseUrl = _configuration["SiteSettings:BaseUrl"];
        var seoMetadata = await _seoService.GenerateSeoMetadataAsync(post, baseUrl!);
        ViewBag.SeoMetadata = seoMetadata;
        ViewBag.JsonLd = _seoService.GenerateJsonLd(post, "BlogPosting");

        return View(post);
    }
}


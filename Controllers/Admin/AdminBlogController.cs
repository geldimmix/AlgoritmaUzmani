using Microsoft.AspNetCore.Mvc;
using AUYeni.Models.Blog;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers.Admin;

[Route("admin/blog")]
public class AdminBlogController : AdminBaseController
{
    private readonly IBlogRepository _blogRepository;
    private readonly ITranslationService _translationService;

    public AdminBlogController(IBlogRepository blogRepository, ITranslationService translationService)
    {
        _blogRepository = blogRepository;
        _translationService = translationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var posts = await _blogRepository.GetAllActiveAsync();
        return View(posts);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(BlogPost post)
    {
        try
        {
            // Translate to English
            post.TitleEn = await _translationService.TranslateToEnglishAsync(post.TitleTr);
            post.ContentEn = await _translationService.TranslateToEnglishAsync(post.ContentTr);
            
            if (!string.IsNullOrEmpty(post.DescriptionTr))
            {
                post.DescriptionEn = await _translationService.TranslateToEnglishAsync(post.DescriptionTr);
            }

            // Generate SEO fields
            post.Slug = await _translationService.GenerateSlugAsync(post.TitleTr);
            post.MetaTitle = post.TitleEn;
            post.MetaDescription = post.DescriptionEn ?? post.ContentEn.Substring(0, Math.Min(160, post.ContentEn.Length));
            post.MetaKeywords = await _translationService.GenerateMetaKeywordsAsync(post.ContentEn);

            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;

            await _blogRepository.CreateAsync(post);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(post);
        }
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _blogRepository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Edit(int id, BlogPost post)
    {
        try
        {
            post.Id = id;
            
            // Translate to English
            post.TitleEn = await _translationService.TranslateToEnglishAsync(post.TitleTr);
            post.ContentEn = await _translationService.TranslateToEnglishAsync(post.ContentTr);
            
            if (!string.IsNullOrEmpty(post.DescriptionTr))
            {
                post.DescriptionEn = await _translationService.TranslateToEnglishAsync(post.DescriptionTr);
            }

            // Update SEO fields
            post.Slug = await _translationService.GenerateSlugAsync(post.TitleTr);
            post.MetaTitle = post.TitleEn;
            post.MetaDescription = post.DescriptionEn ?? post.ContentEn.Substring(0, Math.Min(160, post.ContentEn.Length));
            post.MetaKeywords = await _translationService.GenerateMetaKeywordsAsync(post.ContentEn);

            post.UpdatedAt = DateTime.UtcNow;

            await _blogRepository.UpdateAsync(post);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(post);
        }
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _blogRepository.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}


using Microsoft.AspNetCore.Mvc;
using AUYeni.Models.Tutorial;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers.Admin;

[Route("admin/tutorial")]
public class AdminTutorialController : AdminBaseController
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly ITranslationService _translationService;

    public AdminTutorialController(ITutorialRepository tutorialRepository, ITranslationService translationService)
    {
        _tutorialRepository = tutorialRepository;
        _translationService = translationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tutorials = await _tutorialRepository.GetAllActiveAsync();
        return View(tutorials);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Tutorial tutorial)
    {
        try
        {
            tutorial.TitleEn = await _translationService.TranslateToEnglishAsync(tutorial.TitleTr);
            tutorial.ContentEn = await _translationService.TranslateToEnglishAsync(tutorial.ContentTr);
            if (!string.IsNullOrEmpty(tutorial.DescriptionTr))
                tutorial.DescriptionEn = await _translationService.TranslateToEnglishAsync(tutorial.DescriptionTr);

            tutorial.Slug = await _translationService.GenerateSlugAsync(tutorial.TitleTr);
            tutorial.MetaTitle = tutorial.TitleEn;
            tutorial.MetaDescription = tutorial.DescriptionEn;
            tutorial.MetaKeywords = await _translationService.GenerateMetaKeywordsAsync(tutorial.ContentEn);
            
            tutorial.CreatedAt = DateTime.UtcNow;
            tutorial.UpdatedAt = DateTime.UtcNow;

            await _tutorialRepository.CreateAsync(tutorial);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(tutorial);
        }
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var tutorial = await _tutorialRepository.GetByIdAsync(id);
        if (tutorial == null) return NotFound();
        return View(tutorial);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Edit(int id, Tutorial tutorial)
    {
        try
        {
            tutorial.Id = id;
            tutorial.TitleEn = await _translationService.TranslateToEnglishAsync(tutorial.TitleTr);
            tutorial.ContentEn = await _translationService.TranslateToEnglishAsync(tutorial.ContentTr);
            if (!string.IsNullOrEmpty(tutorial.DescriptionTr))
                tutorial.DescriptionEn = await _translationService.TranslateToEnglishAsync(tutorial.DescriptionTr);

            tutorial.Slug = await _translationService.GenerateSlugAsync(tutorial.TitleTr);
            tutorial.UpdatedAt = DateTime.UtcNow;

            await _tutorialRepository.UpdateAsync(tutorial);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(tutorial);
        }
    }

    [HttpGet("{tutorialId}/sections")]
    public async Task<IActionResult> Sections(int tutorialId)
    {
        ViewBag.TutorialId = tutorialId;
        var sections = await _tutorialRepository.GetSectionsByTutorialAsync(tutorialId);
        return View(sections);
    }

    [HttpGet("section/create/{tutorialId}")]
    public IActionResult CreateSection(int tutorialId)
    {
        ViewBag.TutorialId = tutorialId;
        return View();
    }

    [HttpPost("section/create/{tutorialId}")]
    public async Task<IActionResult> CreateSection(int tutorialId, TutorialSection section)
    {
        try
        {
            section.TutorialId = tutorialId;
            section.TitleEn = await _translationService.TranslateToEnglishAsync(section.TitleTr);
            section.ContentEn = await _translationService.TranslateToEnglishAsync(section.ContentTr);
            section.Slug = await _translationService.GenerateSlugAsync(section.TitleTr);
            section.CreatedAt = DateTime.UtcNow;
            section.UpdatedAt = DateTime.UtcNow;

            await _tutorialRepository.CreateSectionAsync(section);
            return RedirectToAction("Sections", new { tutorialId });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.TutorialId = tutorialId;
            return View(section);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using AUYeni.Models.Dictionary;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers.Admin;

[Route("admin/dictionary")]
public class AdminDictionaryController : AdminBaseController
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly ITranslationService _translationService;

    public AdminDictionaryController(IDictionaryRepository dictionaryRepository, ITranslationService translationService)
    {
        _dictionaryRepository = dictionaryRepository;
        _translationService = translationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var namespaces = await _dictionaryRepository.GetAllNamespacesAsync();
        return View(namespaces);
    }

    [HttpGet("namespace/create")]
    public IActionResult CreateNamespace()
    {
        return View();
    }

    [HttpPost("namespace/create")]
    public async Task<IActionResult> CreateNamespace(DictionaryNamespace ns)
    {
        try
        {
            ns.NameEn = await _translationService.TranslateToEnglishAsync(ns.NameTr);
            if (!string.IsNullOrEmpty(ns.DescriptionTr))
                ns.DescriptionEn = await _translationService.TranslateToEnglishAsync(ns.DescriptionTr);
            
            ns.Slug = await _translationService.GenerateSlugAsync(ns.NameTr);
            ns.CreatedAt = DateTime.UtcNow;
            ns.UpdatedAt = DateTime.UtcNow;

            await _dictionaryRepository.CreateNamespaceAsync(ns);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(ns);
        }
    }

    [HttpGet("namespace/{namespaceId}/entries")]
    public async Task<IActionResult> Entries(int namespaceId)
    {
        ViewBag.NamespaceId = namespaceId;
        var entries = await _dictionaryRepository.GetEntriesByNamespaceAsync(namespaceId);
        return View(entries);
    }

    [HttpGet("entry/create/{namespaceId}")]
    public IActionResult CreateEntry(int namespaceId)
    {
        ViewBag.NamespaceId = namespaceId;
        return View();
    }

    [HttpPost("entry/create/{namespaceId}")]
    public async Task<IActionResult> CreateEntry(int namespaceId, DictionaryEntry entry)
    {
        try
        {
            entry.NamespaceId = namespaceId;
            entry.TermEn = await _translationService.TranslateToEnglishAsync(entry.TermTr);
            entry.ShortDescriptionEn = await _translationService.TranslateToEnglishAsync(entry.ShortDescriptionTr);
            entry.TitleEn = await _translationService.TranslateToEnglishAsync(entry.TitleTr);
            entry.ContentEn = await _translationService.TranslateToEnglishAsync(entry.ContentTr);
            if (!string.IsNullOrEmpty(entry.DescriptionTr))
                entry.DescriptionEn = await _translationService.TranslateToEnglishAsync(entry.DescriptionTr);

            entry.Slug = await _translationService.GenerateSlugAsync(entry.TermTr);
            entry.MetaTitle = entry.TermEn;
            entry.MetaDescription = entry.ShortDescriptionEn;
            entry.MetaKeywords = await _translationService.GenerateMetaKeywordsAsync(entry.ContentEn);
            
            entry.CreatedAt = DateTime.UtcNow;
            entry.UpdatedAt = DateTime.UtcNow;

            await _dictionaryRepository.CreateEntryAsync(entry);
            return RedirectToAction("Entries", new { namespaceId });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.NamespaceId = namespaceId;
            return View(entry);
        }
    }

    [HttpGet("entry/edit/{id}")]
    public async Task<IActionResult> EditEntry(int id)
    {
        var entry = await _dictionaryRepository.GetEntryByIdAsync(id);
        if (entry == null) return NotFound();
        return View(entry);
    }

    [HttpPost("entry/edit/{id}")]
    public async Task<IActionResult> EditEntry(int id, DictionaryEntry entry)
    {
        try
        {
            entry.Id = id;
            entry.TermEn = await _translationService.TranslateToEnglishAsync(entry.TermTr);
            entry.ShortDescriptionEn = await _translationService.TranslateToEnglishAsync(entry.ShortDescriptionTr);
            entry.TitleEn = await _translationService.TranslateToEnglishAsync(entry.TitleTr);
            entry.ContentEn = await _translationService.TranslateToEnglishAsync(entry.ContentTr);
            if (!string.IsNullOrEmpty(entry.DescriptionTr))
                entry.DescriptionEn = await _translationService.TranslateToEnglishAsync(entry.DescriptionTr);

            entry.Slug = await _translationService.GenerateSlugAsync(entry.TermTr);
            entry.UpdatedAt = DateTime.UtcNow;

            await _dictionaryRepository.UpdateEntryAsync(entry);
            return RedirectToAction("Entries", new { namespaceId = entry.NamespaceId });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(entry);
        }
    }
}


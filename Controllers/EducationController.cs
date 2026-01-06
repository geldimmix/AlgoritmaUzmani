using Microsoft.AspNetCore.Mvc;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers;

public class EducationController : Controller
{
    private readonly IEducationRepository _educationRepository;
    private readonly ISeoService _seoService;
    private readonly IConfiguration _configuration;

    public EducationController(IEducationRepository educationRepository, ISeoService seoService, IConfiguration configuration)
    {
        _educationRepository = educationRepository;
        _seoService = seoService;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _educationRepository.GetAllCoursesAsync();
        return View(courses);
    }

    [Route("education/{courseSlug}/{sectionSlug?}/{lessonSlug?}")]
    public async Task<IActionResult> Course(string courseSlug, string? sectionSlug, string? lessonSlug)
    {
        var course = await _educationRepository.GetCourseBySlugAsync(courseSlug);
        if (course == null)
        {
            return NotFound();
        }

        var sections = await _educationRepository.GetSectionsByCourseAsync(course.Id);
        ViewBag.Sections = sections;

        if (!string.IsNullOrEmpty(lessonSlug))
        {
            var lesson = await _educationRepository.GetLessonBySlugAsync(lessonSlug);
            if (lesson != null)
            {
                var baseUrl = _configuration["SiteSettings:BaseUrl"];
                var seoMetadata = await _seoService.GenerateSeoMetadataAsync(lesson, baseUrl!);
                ViewBag.SeoMetadata = seoMetadata;
                ViewBag.JsonLd = _seoService.GenerateJsonLd(lesson, "LearningResource");
                ViewBag.CurrentLesson = lesson;
            }
        }
        else
        {
            var baseUrl = _configuration["SiteSettings:BaseUrl"];
            var seoMetadata = await _seoService.GenerateSeoMetadataAsync(course, baseUrl!);
            ViewBag.SeoMetadata = seoMetadata;
            ViewBag.JsonLd = _seoService.GenerateJsonLd(course, "Course");
        }

        return View(course);
    }
}


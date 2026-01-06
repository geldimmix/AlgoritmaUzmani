using Microsoft.AspNetCore.Mvc;
using AUYeni.Models.Education;
using AUYeni.Repositories;
using AUYeni.Services;

namespace AUYeni.Controllers.Admin;

[Route("admin/education")]
public class AdminEducationController : AdminBaseController
{
    private readonly IEducationRepository _educationRepository;
    private readonly ITranslationService _translationService;

    public AdminEducationController(IEducationRepository educationRepository, ITranslationService translationService)
    {
        _educationRepository = educationRepository;
        _translationService = translationService;
    }

    // Courses
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var courses = await _educationRepository.GetAllCoursesAsync();
        return View(courses);
    }

    [HttpGet("course/create")]
    public IActionResult CreateCourse()
    {
        return View();
    }

    [HttpPost("course/create")]
    public async Task<IActionResult> CreateCourse(Course course)
    {
        try
        {
            course.TitleEn = await _translationService.TranslateToEnglishAsync(course.TitleTr);
            course.ContentEn = await _translationService.TranslateToEnglishAsync(course.ContentTr);
            if (!string.IsNullOrEmpty(course.DescriptionTr))
                course.DescriptionEn = await _translationService.TranslateToEnglishAsync(course.DescriptionTr);

            course.Slug = await _translationService.GenerateSlugAsync(course.TitleTr);
            course.MetaTitle = course.TitleEn;
            course.MetaDescription = course.DescriptionEn;
            course.MetaKeywords = await _translationService.GenerateMetaKeywordsAsync(course.ContentEn);
            
            course.CreatedAt = DateTime.UtcNow;
            course.UpdatedAt = DateTime.UtcNow;

            await _educationRepository.CreateCourseAsync(course);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(course);
        }
    }

    [HttpGet("course/edit/{id}")]
    public async Task<IActionResult> EditCourse(int id)
    {
        var course = await _educationRepository.GetCourseByIdAsync(id);
        if (course == null) return NotFound();
        return View(course);
    }

    [HttpPost("course/edit/{id}")]
    public async Task<IActionResult> EditCourse(int id, Course course)
    {
        try
        {
            course.Id = id;
            course.TitleEn = await _translationService.TranslateToEnglishAsync(course.TitleTr);
            course.ContentEn = await _translationService.TranslateToEnglishAsync(course.ContentTr);
            if (!string.IsNullOrEmpty(course.DescriptionTr))
                course.DescriptionEn = await _translationService.TranslateToEnglishAsync(course.DescriptionTr);

            course.Slug = await _translationService.GenerateSlugAsync(course.TitleTr);
            course.UpdatedAt = DateTime.UtcNow;

            await _educationRepository.UpdateCourseAsync(course);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(course);
        }
    }

    // Sections
    [HttpGet("course/{courseId}/sections")]
    public async Task<IActionResult> Sections(int courseId)
    {
        ViewBag.CourseId = courseId;
        var sections = await _educationRepository.GetSectionsByCourseAsync(courseId);
        return View(sections);
    }

    [HttpGet("section/create/{courseId}")]
    public IActionResult CreateSection(int courseId)
    {
        ViewBag.CourseId = courseId;
        return View();
    }

    [HttpPost("section/create/{courseId}")]
    public async Task<IActionResult> CreateSection(int courseId, Section section)
    {
        try
        {
            section.CourseId = courseId;
            section.TitleEn = await _translationService.TranslateToEnglishAsync(section.TitleTr);
            if (!string.IsNullOrEmpty(section.DescriptionTr))
                section.DescriptionEn = await _translationService.TranslateToEnglishAsync(section.DescriptionTr);
            
            section.Slug = await _translationService.GenerateSlugAsync(section.TitleTr);
            section.CreatedAt = DateTime.UtcNow;
            section.UpdatedAt = DateTime.UtcNow;

            await _educationRepository.CreateSectionAsync(section);
            return RedirectToAction("Sections", new { courseId });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.CourseId = courseId;
            return View(section);
        }
    }

    // Lessons
    [HttpGet("section/{sectionId}/lessons")]
    public async Task<IActionResult> Lessons(int sectionId)
    {
        ViewBag.SectionId = sectionId;
        var lessons = await _educationRepository.GetLessonsBySectionAsync(sectionId);
        return View(lessons);
    }

    [HttpGet("lesson/create/{sectionId}")]
    public IActionResult CreateLesson(int sectionId)
    {
        ViewBag.SectionId = sectionId;
        return View();
    }

    [HttpPost("lesson/create/{sectionId}")]
    public async Task<IActionResult> CreateLesson(int sectionId, Lesson lesson)
    {
        try
        {
            lesson.SectionId = sectionId;
            lesson.TitleEn = await _translationService.TranslateToEnglishAsync(lesson.TitleTr);
            lesson.ContentEn = await _translationService.TranslateToEnglishAsync(lesson.ContentTr);
            if (!string.IsNullOrEmpty(lesson.DescriptionTr))
                lesson.DescriptionEn = await _translationService.TranslateToEnglishAsync(lesson.DescriptionTr);

            lesson.Slug = await _translationService.GenerateSlugAsync(lesson.TitleTr);
            lesson.MetaTitle = lesson.TitleEn;
            lesson.MetaDescription = lesson.DescriptionEn;
            lesson.MetaKeywords = await _translationService.GenerateMetaKeywordsAsync(lesson.ContentEn);
            
            lesson.CreatedAt = DateTime.UtcNow;
            lesson.UpdatedAt = DateTime.UtcNow;

            await _educationRepository.CreateLessonAsync(lesson);
            return RedirectToAction("Lessons", new { sectionId });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.SectionId = sectionId;
            return View(lesson);
        }
    }
}


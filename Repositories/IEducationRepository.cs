using AUYeni.Models.Education;

namespace AUYeni.Repositories;

public interface IEducationRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseByIdAsync(int id);
    Task<Course?> GetCourseBySlugAsync(string slug);
    Task<int> CreateCourseAsync(Course course);
    Task<bool> UpdateCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(int id);
    
    Task<IEnumerable<Section>> GetSectionsByCourseAsync(int courseId);
    Task<Section?> GetSectionByIdAsync(int id);
    Task<int> CreateSectionAsync(Section section);
    Task<bool> UpdateSectionAsync(Section section);
    Task<bool> DeleteSectionAsync(int id);
    
    Task<IEnumerable<Lesson>> GetLessonsBySectionAsync(int sectionId);
    Task<Lesson?> GetLessonByIdAsync(int id);
    Task<Lesson?> GetLessonBySlugAsync(string slug);
    Task<int> CreateLessonAsync(Lesson lesson);
    Task<bool> UpdateLessonAsync(Lesson lesson);
    Task<bool> DeleteLessonAsync(int id);
}


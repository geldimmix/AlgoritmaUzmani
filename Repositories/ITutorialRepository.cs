using AUYeni.Models.Tutorial;

namespace AUYeni.Repositories;

public interface ITutorialRepository
{
    Task<IEnumerable<Tutorial>> GetAllActiveAsync();
    Task<Tutorial?> GetByIdAsync(int id);
    Task<Tutorial?> GetBySlugAsync(string slug);
    Task<int> CreateAsync(Tutorial tutorial);
    Task<bool> UpdateAsync(Tutorial tutorial);
    Task<bool> DeleteAsync(int id);
    
    Task<IEnumerable<TutorialSection>> GetSectionsByTutorialAsync(int tutorialId);
    Task<TutorialSection?> GetSectionByIdAsync(int id);
    Task<int> CreateSectionAsync(TutorialSection section);
    Task<bool> UpdateSectionAsync(TutorialSection section);
    Task<bool> DeleteSectionAsync(int id);
}


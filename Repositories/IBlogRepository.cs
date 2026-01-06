using AUYeni.Models.Blog;

namespace AUYeni.Repositories;

public interface IBlogRepository
{
    Task<IEnumerable<BlogPost>> GetAllActiveAsync();
    Task<BlogPost?> GetByIdAsync(int id);
    Task<BlogPost?> GetBySlugAsync(string slug);
    Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId);
    Task<int> CreateAsync(BlogPost post);
    Task<bool> UpdateAsync(BlogPost post);
    Task<bool> DeleteAsync(int id);
    Task<bool> AddTagsAsync(int postId, List<int> tagIds);
    Task<bool> RemoveTagsAsync(int postId);
    Task<bool> IncrementViewCountAsync(int id);
}


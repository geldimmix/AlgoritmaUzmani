using AUYeni.Models.Dictionary;

namespace AUYeni.Repositories;

public interface IDictionaryRepository
{
    Task<IEnumerable<DictionaryNamespace>> GetAllNamespacesAsync();
    Task<DictionaryNamespace?> GetNamespaceByIdAsync(int id);
    Task<DictionaryNamespace?> GetNamespaceBySlugAsync(string slug);
    Task<int> CreateNamespaceAsync(DictionaryNamespace ns);
    Task<bool> UpdateNamespaceAsync(DictionaryNamespace ns);
    Task<bool> DeleteNamespaceAsync(int id);
    
    Task<IEnumerable<DictionaryEntry>> GetAllEntriesAsync();
    Task<IEnumerable<DictionaryEntry>> GetEntriesByNamespaceAsync(int namespaceId);
    Task<DictionaryEntry?> GetEntryByIdAsync(int id);
    Task<DictionaryEntry?> GetEntryBySlugAsync(string slug, int namespaceId);
    Task<int> CreateEntryAsync(DictionaryEntry entry);
    Task<bool> UpdateEntryAsync(DictionaryEntry entry);
    Task<bool> DeleteEntryAsync(int id);
}


namespace AUYeni.Services;

public interface ITranslationService
{
    Task<string> TranslateToEnglishAsync(string turkishText);
    Task<string> GenerateSlugAsync(string text);
    Task<string> GenerateMetaKeywordsAsync(string content);
}


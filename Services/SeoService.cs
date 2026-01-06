using AUYeni.Models;
using Newtonsoft.Json;

namespace AUYeni.Services;

public class SeoService : ISeoService
{
    private readonly IConfiguration _configuration;

    public SeoService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<SeoMetadata> GenerateSeoMetadataAsync(SeoEntity entity, string baseUrl)
    {
        var metadata = new SeoMetadata
        {
            Title = !string.IsNullOrEmpty(entity.MetaTitle) ? entity.MetaTitle : entity.TitleEn,
            Description = !string.IsNullOrEmpty(entity.MetaDescription) 
                ? entity.MetaDescription 
                : (entity.DescriptionEn ?? GenerateMetaDescription(entity.ContentEn)),
            Keywords = entity.MetaKeywords ?? string.Empty,
            CanonicalUrl = $"{baseUrl}/{entity.Slug}",
            OgTitle = entity.TitleEn,
            OgDescription = entity.DescriptionEn ?? GenerateMetaDescription(entity.ContentEn),
            OgImage = entity.OgImage,
            OgUrl = $"{baseUrl}/{entity.Slug}"
        };

        return await Task.FromResult(metadata);
    }

    public string GenerateCanonicalUrl(string slug, string module)
    {
        var baseUrl = _configuration["SiteSettings:BaseUrl"];
        return $"{baseUrl}/{module}/{slug}";
    }

    public string GenerateJsonLd(SeoEntity entity, string type)
    {
        var jsonLd = new
        {
            context = "https://schema.org",
            type = type,
            headline = entity.TitleEn,
            description = entity.DescriptionEn,
            datePublished = entity.CreatedAt.ToString("yyyy-MM-dd"),
            dateModified = entity.UpdatedAt.ToString("yyyy-MM-dd"),
            image = entity.OgImage
        };

        return JsonConvert.SerializeObject(jsonLd);
    }

    private string GenerateMetaDescription(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return string.Empty;

        // Strip HTML tags
        var text = System.Text.RegularExpressions.Regex.Replace(content, "<.*?>", string.Empty);
        
        // Take first 160 characters
        return text.Length > 160 ? text.Substring(0, 157) + "..." : text;
    }
}


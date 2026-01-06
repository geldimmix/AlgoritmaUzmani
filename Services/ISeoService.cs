using AUYeni.Models;

namespace AUYeni.Services;

public interface ISeoService
{
    Task<SeoMetadata> GenerateSeoMetadataAsync(SeoEntity entity, string baseUrl);
    string GenerateCanonicalUrl(string slug, string module);
    string GenerateJsonLd(SeoEntity entity, string type);
}

public class SeoMetadata
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string CanonicalUrl { get; set; } = string.Empty;
    public string OgTitle { get; set; } = string.Empty;
    public string OgDescription { get; set; } = string.Empty;
    public string? OgImage { get; set; }
    public string OgUrl { get; set; } = string.Empty;
    public string JsonLd { get; set; } = string.Empty;
}


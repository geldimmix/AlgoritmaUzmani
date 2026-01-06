namespace AUYeni.Models;

public abstract class SeoEntity : BaseEntity
{
    // Turkish Input
    public string TitleTr { get; set; } = string.Empty;
    public string ContentTr { get; set; } = string.Empty;
    public string? DescriptionTr { get; set; }
    
    // English (SEO)
    public string TitleEn { get; set; } = string.Empty;
    public string ContentEn { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; }
    
    // SEO Fields
    public string Slug { get; set; } = string.Empty;
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? OgImage { get; set; }
    
    // Analytics
    public int ViewCount { get; set; } = 0;
}


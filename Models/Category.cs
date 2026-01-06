namespace AUYeni.Models;

public class Category : BaseEntity
{
    public string NameTr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? DescriptionTr { get; set; }
    public string? DescriptionEn { get; set; }
    public string ModuleType { get; set; } = string.Empty; // Blog, Education, Tutorial
}


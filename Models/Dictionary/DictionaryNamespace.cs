namespace AUYeni.Models.Dictionary;

public class DictionaryNamespace : BaseEntity
{
    public string NameTr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? DescriptionTr { get; set; }
    public string? DescriptionEn { get; set; }
    
    // Navigation properties
    public List<DictionaryEntry> Entries { get; set; } = new();
}


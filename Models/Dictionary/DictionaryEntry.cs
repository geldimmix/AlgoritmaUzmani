namespace AUYeni.Models.Dictionary;

public class DictionaryEntry : SeoEntity
{
    public int NamespaceId { get; set; }
    public string TermTr { get; set; } = string.Empty;
    public string TermEn { get; set; } = string.Empty;
    public string ShortDescriptionTr { get; set; } = string.Empty;
    public string ShortDescriptionEn { get; set; } = string.Empty;
    
    // Navigation properties
    public DictionaryNamespace Namespace { get; set; } = null!;
}


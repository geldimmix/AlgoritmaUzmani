namespace AUYeni.Models.Tutorial;

public class TutorialSection : BaseEntity
{
    public int TutorialId { get; set; }
    public string TitleTr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string ContentTr { get; set; } = string.Empty;
    public string ContentEn { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int Order { get; set; }
    
    // Navigation properties
    public Tutorial Tutorial { get; set; } = null!;
}


namespace AUYeni.Models.Tutorial;

public class Tutorial : SeoEntity
{
    public int? CategoryId { get; set; }
    public string? FeaturedImage { get; set; }
    public string Difficulty { get; set; } = "Beginner";
    
    // Navigation properties
    public Category? Category { get; set; }
    public List<TutorialTag> TutorialTags { get; set; } = new();
    public List<TutorialSection> Sections { get; set; } = new();
}

public class TutorialTag
{
    public int TutorialId { get; set; }
    public int TagId { get; set; }
    
    public Tutorial Tutorial { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}


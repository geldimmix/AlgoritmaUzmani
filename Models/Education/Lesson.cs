namespace AUYeni.Models.Education;

public class Lesson : SeoEntity
{
    public int SectionId { get; set; }
    public int Order { get; set; }
    public int DurationMinutes { get; set; }
    public string? VideoUrl { get; set; }
    
    // Navigation properties
    public Section Section { get; set; } = null!;
}


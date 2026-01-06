namespace AUYeni.Models.Education;

public class Section : BaseEntity
{
    public int CourseId { get; set; }
    public string TitleTr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? DescriptionTr { get; set; }
    public string? DescriptionEn { get; set; }
    public int Order { get; set; }
    
    // Navigation properties
    public Course Course { get; set; } = null!;
    public List<Lesson> Lessons { get; set; } = new();
}


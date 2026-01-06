namespace AUYeni.Models.Education;

public class Course : SeoEntity
{
    public int? CategoryId { get; set; }
    public string? FeaturedImage { get; set; }
    public string? InstructorName { get; set; }
    public string Level { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced
    
    // Navigation properties
    public Category? Category { get; set; }
    public List<CourseTag> CourseTags { get; set; } = new();
    public List<Section> Sections { get; set; } = new();
}

public class CourseTag
{
    public int CourseId { get; set; }
    public int TagId { get; set; }
    
    public Course Course { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}


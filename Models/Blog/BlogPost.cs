namespace AUYeni.Models.Blog;

public class BlogPost : SeoEntity
{
    public int? CategoryId { get; set; }
    public string? FeaturedImage { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string? AuthorName { get; set; }
    
    // Navigation properties
    public Category? Category { get; set; }
    public List<BlogPostTag> BlogPostTags { get; set; } = new();
}

public class BlogPostTag
{
    public int BlogPostId { get; set; }
    public int TagId { get; set; }
    
    public BlogPost BlogPost { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}


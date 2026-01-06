using Microsoft.EntityFrameworkCore;
using AUYeni.Models;
using AUYeni.Models.Blog;
using AUYeni.Models.Education;
using AUYeni.Models.Tutorial;
using AUYeni.Models.Dictionary;

namespace AUYeni.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Common
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    // Blog
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<BlogPostTag> BlogPostTags { get; set; }
    
    // Education
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseTag> CourseTags { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    
    // Tutorial
    public DbSet<Tutorial> Tutorials { get; set; }
    public DbSet<TutorialTag> TutorialTags { get; set; }
    public DbSet<TutorialSection> TutorialSections { get; set; }
    
    // Dictionary
    public DbSet<DictionaryNamespace> DictionaryNamespaces { get; set; }
    public DbSet<DictionaryEntry> DictionaryEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure table names (PostgreSQL lowercase convention)
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Tag>().ToTable("tags");
        modelBuilder.Entity<BlogPost>().ToTable("blog_posts");
        modelBuilder.Entity<BlogPostTag>().ToTable("blog_post_tags");
        modelBuilder.Entity<Course>().ToTable("courses");
        modelBuilder.Entity<CourseTag>().ToTable("course_tags");
        modelBuilder.Entity<Section>().ToTable("sections");
        modelBuilder.Entity<Lesson>().ToTable("lessons");
        modelBuilder.Entity<Tutorial>().ToTable("tutorials");
        modelBuilder.Entity<TutorialTag>().ToTable("tutorial_tags");
        modelBuilder.Entity<TutorialSection>().ToTable("tutorial_sections");
        modelBuilder.Entity<DictionaryNamespace>().ToTable("dictionary_namespaces");
        modelBuilder.Entity<DictionaryEntry>().ToTable("dictionary_entries");

        // BlogPostTag many-to-many
        modelBuilder.Entity<BlogPostTag>()
            .HasKey(bt => new { bt.BlogPostId, bt.TagId });
        
        modelBuilder.Entity<BlogPostTag>()
            .HasOne(bt => bt.BlogPost)
            .WithMany(b => b.BlogPostTags)
            .HasForeignKey(bt => bt.BlogPostId);
        
        modelBuilder.Entity<BlogPostTag>()
            .HasOne(bt => bt.Tag)
            .WithMany()
            .HasForeignKey(bt => bt.TagId);

        // CourseTag many-to-many
        modelBuilder.Entity<CourseTag>()
            .HasKey(ct => new { ct.CourseId, ct.TagId });
        
        modelBuilder.Entity<CourseTag>()
            .HasOne(ct => ct.Course)
            .WithMany(c => c.CourseTags)
            .HasForeignKey(ct => ct.CourseId);
        
        modelBuilder.Entity<CourseTag>()
            .HasOne(ct => ct.Tag)
            .WithMany()
            .HasForeignKey(ct => ct.TagId);

        // TutorialTag many-to-many
        modelBuilder.Entity<TutorialTag>()
            .HasKey(tt => new { tt.TutorialId, tt.TagId });
        
        modelBuilder.Entity<TutorialTag>()
            .HasOne(tt => tt.Tutorial)
            .WithMany(t => t.TutorialTags)
            .HasForeignKey(tt => tt.TutorialId);
        
        modelBuilder.Entity<TutorialTag>()
            .HasOne(tt => tt.Tag)
            .WithMany()
            .HasForeignKey(tt => tt.TagId);

        // Indexes for SEO
        modelBuilder.Entity<BlogPost>().HasIndex(b => b.Slug).IsUnique();
        modelBuilder.Entity<Course>().HasIndex(c => c.Slug).IsUnique();
        modelBuilder.Entity<Tutorial>().HasIndex(t => t.Slug).IsUnique();
        modelBuilder.Entity<Lesson>().HasIndex(l => l.Slug);
        modelBuilder.Entity<DictionaryEntry>().HasIndex(d => d.Slug);
        modelBuilder.Entity<Category>().HasIndex(c => c.Slug);
        modelBuilder.Entity<Tag>().HasIndex(t => t.Slug);
    }
}


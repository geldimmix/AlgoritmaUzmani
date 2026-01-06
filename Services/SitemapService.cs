using System.Text;
using System.Xml;
using AUYeni.Repositories;

namespace AUYeni.Services;

public class SitemapService : ISitemapService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly ITutorialRepository _tutorialRepository;
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IConfiguration _configuration;

    public SitemapService(
        IBlogRepository blogRepository,
        IEducationRepository educationRepository,
        ITutorialRepository tutorialRepository,
        IDictionaryRepository dictionaryRepository,
        IConfiguration configuration)
    {
        _blogRepository = blogRepository;
        _educationRepository = educationRepository;
        _tutorialRepository = tutorialRepository;
        _dictionaryRepository = dictionaryRepository;
        _configuration = configuration;
    }

    public async Task<string> GenerateSitemapXmlAsync()
    {
        var baseUrl = _configuration["SiteSettings:BaseUrl"];
        var sb = new StringBuilder();
        
        var settings = new XmlWriterSettings
        {
            Indent = true,
            Encoding = Encoding.UTF8
        };

        using (var writer = XmlWriter.Create(sb, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            // Home page
            WriteUrl(writer, baseUrl!, DateTime.UtcNow, "1.0", "daily");

            // Blog posts
            var blogPosts = await _blogRepository.GetAllActiveAsync();
            foreach (var post in blogPosts)
            {
                WriteUrl(writer, $"{baseUrl}/blog/{post.Slug}", post.UpdatedAt, "0.8", "weekly");
            }

            // Courses
            var courses = await _educationRepository.GetAllCoursesAsync();
            foreach (var course in courses)
            {
                WriteUrl(writer, $"{baseUrl}/education/{course.Slug}", course.UpdatedAt, "0.8", "weekly");
            }

            // Tutorials
            var tutorials = await _tutorialRepository.GetAllActiveAsync();
            foreach (var tutorial in tutorials)
            {
                WriteUrl(writer, $"{baseUrl}/tutorial/{tutorial.Slug}", tutorial.UpdatedAt, "0.8", "weekly");
            }

            // Dictionary entries
            var entries = await _dictionaryRepository.GetAllEntriesAsync();
            foreach (var entry in entries)
            {
                var ns = await _dictionaryRepository.GetNamespaceByIdAsync(entry.NamespaceId);
                WriteUrl(writer, $"{baseUrl}/dictionary/{ns?.Slug}/{entry.Slug}", entry.UpdatedAt, "0.7", "monthly");
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        return sb.ToString();
    }

    private void WriteUrl(XmlWriter writer, string loc, DateTime lastMod, string priority, string changeFreq)
    {
        writer.WriteStartElement("url");
        writer.WriteElementString("loc", loc);
        writer.WriteElementString("lastmod", lastMod.ToString("yyyy-MM-dd"));
        writer.WriteElementString("priority", priority);
        writer.WriteElementString("changefreq", changeFreq);
        writer.WriteEndElement();
    }
}


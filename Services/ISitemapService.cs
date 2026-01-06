namespace AUYeni.Services;

public interface ISitemapService
{
    Task<string> GenerateSitemapXmlAsync();
}


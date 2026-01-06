using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AUYeni.Services;

public class DeepInfraTranslationService : ITranslationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiKey;
    private readonly string _apiUrl;
    private readonly string _model;

    public DeepInfraTranslationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _apiKey = _configuration["DeepInfra:ApiKey"] ?? throw new ArgumentNullException("DeepInfra:ApiKey");
        _apiUrl = _configuration["DeepInfra:ApiUrl"] ?? throw new ArgumentNullException("DeepInfra:ApiUrl");
        _model = _configuration["DeepInfra:Model"] ?? throw new ArgumentNullException("DeepInfra:Model");
    }

    public async Task<string> TranslateToEnglishAsync(string turkishText)
    {
        if (string.IsNullOrWhiteSpace(turkishText))
            return string.Empty;

        var requestBody = new
        {
            model = _model,
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = $"Translate the following Turkish text to English. Only return the translated text, nothing else:\n\n{turkishText}"
                }
            }
        };

        var json = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.PostAsync(_apiUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Translation API error: {responseContent}");
        }

        var result = JsonConvert.DeserializeObject<DeepInfraResponse>(responseContent);
        return result?.Choices?.FirstOrDefault()?.Message?.Content?.Trim() ?? turkishText;
    }

    public async Task<string> GenerateSlugAsync(string text)
    {
        // First translate to English
        var englishText = await TranslateToEnglishAsync(text);
        
        // Convert to slug
        var slug = englishText.ToLowerInvariant();
        
        // Remove special characters
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        
        // Replace spaces with hyphens
        slug = Regex.Replace(slug, @"\s+", "-");
        
        // Remove multiple hyphens
        slug = Regex.Replace(slug, @"-+", "-");
        
        // Trim hyphens from start and end
        slug = slug.Trim('-');
        
        return slug;
    }

    public async Task<string> GenerateMetaKeywordsAsync(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return string.Empty;

        var requestBody = new
        {
            model = _model,
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = $"Extract 5-10 SEO keywords from the following content. Return only the keywords separated by commas:\n\n{content.Substring(0, Math.Min(1000, content.Length))}"
                }
            }
        };

        var json = JsonConvert.SerializeObject(requestBody);
        var contentRequest = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.PostAsync(_apiUrl, contentRequest);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return string.Empty;
        }

        var result = JsonConvert.DeserializeObject<DeepInfraResponse>(responseContent);
        return result?.Choices?.FirstOrDefault()?.Message?.Content?.Trim() ?? string.Empty;
    }
}

// Response models for DeepInfra API
public class DeepInfraResponse
{
    public List<Choice>? Choices { get; set; }
}

public class Choice
{
    public Message? Message { get; set; }
}

public class Message
{
    public string? Content { get; set; }
}


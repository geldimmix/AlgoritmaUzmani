namespace AUYeni.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;
    private readonly string _uploadPath;

    public FileUploadService(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
        
        // AppData klasörü yolu
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _uploadPath = Path.Combine(appDataPath, "AUYeni", "uploads");
        
        // Klasör yoksa oluştur
        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty");
        }

        // Allowed extensions
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        
        if (!allowedExtensions.Contains(extension))
        {
            throw new ArgumentException("Invalid file type. Only images are allowed.");
        }

        // Generate unique filename
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(_uploadPath, fileName);

        // Save file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public bool DeleteImage(string fileName)
    {
        try
        {
            var filePath = Path.Combine(_uploadPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public string GetImageUrl(string fileName)
    {
        var baseUrl = _configuration["SiteSettings:BaseUrl"];
        return $"{baseUrl}/uploads/{fileName}";
    }
}


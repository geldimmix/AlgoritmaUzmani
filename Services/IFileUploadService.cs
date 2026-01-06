namespace AUYeni.Services;

public interface IFileUploadService
{
    Task<string> UploadImageAsync(IFormFile file);
    bool DeleteImage(string fileName);
    string GetImageUrl(string fileName);
}


using Microsoft.AspNetCore.Mvc;

namespace AUYeni.Controllers;

[Route("uploads")]
public class UploadsController : Controller
{
    [HttpGet("{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var filePath = Path.Combine(appDataPath, "AUYeni", "uploads", fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        var contentType = extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };

        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, contentType);
    }
}


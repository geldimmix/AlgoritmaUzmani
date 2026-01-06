using Microsoft.AspNetCore.Mvc;
using AUYeni.Services;

namespace AUYeni.Controllers.Admin;

[Route("admin/upload")]
public class UploadController : AdminBaseController
{
    private readonly IFileUploadService _fileUploadService;

    public UploadController(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            var fileName = await _fileUploadService.UploadImageAsync(file);
            var url = _fileUploadService.GetImageUrl(fileName);
            
            return Json(new { success = true, url, fileName });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpDelete("image/{fileName}")]
    public IActionResult DeleteImage(string fileName)
    {
        var result = _fileUploadService.DeleteImage(fileName);
        return Json(new { success = result });
    }
}


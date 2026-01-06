using Microsoft.AspNetCore.Mvc;

namespace AUYeni.Controllers.Admin;

public class DashboardController : Controller
{
    [HttpGet("/admin/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/admin/login")]
    public IActionResult Login(string username, string password)
    {
        // Simple login - enhance with proper authentication
        if (username == "admin" && password == "Admin123!")
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Index");
        }
        
        ViewBag.Error = "Invalid credentials";
        return View();
    }

    [HttpGet("/admin")]
    [HttpGet("/admin/dashboard")]
    public IActionResult Index()
    {
        if (!HttpContext.Session.GetString("IsAdmin")?.Equals("true") ?? true)
        {
            return RedirectToAction("Login");
        }
        
        return View();
    }

    [HttpPost("/admin/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}


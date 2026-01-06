using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AUYeni.Controllers.Admin;

public class AdminBaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Simple authentication check - you can enhance this with proper authentication
        if (!HttpContext.Session.GetString("IsAdmin")?.Equals("true") ?? true)
        {
            // Allow login page
            if (context.ActionDescriptor.RouteValues["action"] != "Login")
            {
                context.Result = RedirectToAction("Login", "Dashboard");
            }
        }
        
        base.OnActionExecuting(context);
    }
}


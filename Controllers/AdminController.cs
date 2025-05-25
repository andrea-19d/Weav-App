using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weav_App.Controllers.Filters;
using Weav_App.DTOs;

namespace Weav_App.Controllers;

public class AdminController : Controller
{
    [UserLevelAuthorize(UserLevel.Admin)]
    public IActionResult Index()
    {
        return View();
    }
    
}
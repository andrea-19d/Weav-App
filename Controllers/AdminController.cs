using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weav_App.Controllers.Filters;
using Weav_App.DTOs;

namespace Weav_App.Controllers;

[UserLevelAuthorize(UserLevel.Admin)]
public class AdminController : Controller
{


    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult AdminAccount()
    {
        return View();
    }

    public IActionResult ProductManagement()
    {
        return View();
    }

    public IActionResult OrderPage()
    {
        return View();
    }
    
}
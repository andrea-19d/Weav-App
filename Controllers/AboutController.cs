using Microsoft.AspNetCore.Mvc;

namespace Weav_App.Controllers;

public class AboutController : Controller
{
    private readonly ILogger<AboutController> _logger;

    public AboutController(ILogger<AboutController> logger)
    {
        _logger = logger;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
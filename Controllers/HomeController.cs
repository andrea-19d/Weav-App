using System.Diagnostics;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Weav_App.Config;
using Weav_App.Models;

namespace Weav_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly FirebaseAuthProvider _authProvider;

    public HomeController(ILogger<HomeController> logger, IOptions<FirebaseSettings> options)
    {
        _logger = logger;
        string apiKey = options.Value.ApiKey;
        _authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
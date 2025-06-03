using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Weav_App.DTOs.Entities.User;
using Weav_App.Models;
using Weav_App.Services.Interface;

namespace Weav_App.Controllers;

public class LoginController : Controller
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;


    public LoginController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    // GET: Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    // POST: Log in
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LogInUser model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var newUser = _mapper.Map<LoginUserDTO>(model);
        var user = await _authService.LoginUserAsync(newUser, newUser.Password);
        var userLevel = user.level.ToString();
        Console.WriteLine($"{userLevel}");
        Console.WriteLine($"Logged user: {model.UserName}");
        
        if (user.succes != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim("UserLevel", user.level.ToString()) 
            };
            Console.WriteLine($"Logging in user with level: {user.level}");

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            var identity = new ClaimsIdentity(claims, "login");
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : (DateTimeOffset?)null
            };
            
            

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Login");
    }
}
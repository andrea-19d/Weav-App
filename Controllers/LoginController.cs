using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Weav_App.DTOs;
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

        var loginDto = _mapper.Map<LoginUserDto>(model);
        if (model.Level.Equals("Admin"))
        {
            
        }
        
        var user = await _authService.LoginUser(loginDto, loginDto.Password);

        if (user.succes == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.UserName),
            new Claim("UserLevel", user.level.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = model.RememberMe,
            ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : (DateTimeOffset?)null
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        return user.level switch
        {
            UserLevel.Admin => RedirectToAction("Dashboard", "Admin"),
            UserLevel.User => RedirectToAction("Index", "Home"),
            _ => RedirectToAction("Unauthorized", "Home")
        };
    }

    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Login");
    }
}
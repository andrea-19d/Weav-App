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
    private readonly IUserService _userService;
    private readonly IMapper _mapper;


    public LoginController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
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
        var user = await _userService.LoginUserAsync(newUser, model.Password);
        
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                // Add other claims as needed
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

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
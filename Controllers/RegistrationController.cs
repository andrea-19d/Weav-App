using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Weav_App.DTOs.Entities.User;
using Weav_App.Models;
using Weav_App.Services;
using Weav_App.Services.Interface;

namespace Weav_App.Controllers;

public class RegistrationController : Controller
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RegistrationController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    
    // GET: Register
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterUser());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterUser model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            // Map RegisterUser model to RegisterUserDTO
            var registerUserDto = _mapper.Map<RegisterUserDto>(model);

            // Perform registration via the user service
            var result = await _authService.RegisterUser(registerUserDto);

            if (result.succes)
            {
                // Redirect to a confirmation page or login page
                return RedirectToAction("Login", "Login");
            }
            else
            {
                // Add an error message to the model state
                ModelState.AddModelError("", $"Registration failed: {result.error}");
                return View(model);
            }
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            // Handle exception gracefully
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View(model);
        }
    }
}
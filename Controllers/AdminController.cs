using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Weav_App.Controllers.Filters;
using Weav_App.DTOs;
using Weav_App.Helpers;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.Interface;

namespace Weav_App.Controllers;

[UserLevelAuthorize(UserLevel.Admin)]
public class AdminController : Controller
{
    private readonly IAdminProductManagementPageService _adminProductManagementPageService;
    private readonly IAdminOrderPageService _adminOrderPageService;
    private readonly ICategoryServices _categoryServices;
    private readonly UsefulChecks _checks;
    private readonly IAdminUserManagementPage _adminUserManagementPage;


    public AdminController(IAdminProductManagementPageService adminProductManagementPageService,
        ICategoryServices categoryServices, IAdminOrderPageService adminOrderPageService,
        IAdminUserManagementPage adminUserManagementPage)
    {
        _categoryServices = categoryServices;
        _adminProductManagementPageService = adminProductManagementPageService;
        _adminOrderPageService = adminOrderPageService;
        _adminUserManagementPage = adminUserManagementPage;
    }

    [HttpGet]
    public IActionResult AddAdmin()
    {
        return View("Forms/AddAdmin");
    }

    [HttpPost]
    public async Task<IActionResult> AddAdmin(RegisterUserModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Model is invalid";
            return View("Forms/AddAdmin", model);
        }

        try
        {
            var result = await _adminUserManagementPage.AddAdmin(model);
            if (result.Success == true && result.Data != null)
            {
                TempData["SuccessMessage"] = result.ErrorMessage;
                Console.WriteLine("Admin added successfully");
            }

            TempData["ErrorMessage"] = result.ErrorMessage;
            return RedirectToAction("AddAdmin");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Error while adding admin";
            return BadRequest(new { message = ex.Message });
        }
    }
    
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult AdminAccount()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ProductManagement(string? searchQuery, string? selectedCategory, int page = 1,
        int pageSize = 10)
    {
        var viewModel =
            await _adminProductManagementPageService.GetProductManagementViewModel(searchQuery, selectedCategory, page,
                pageSize);

        if (!viewModel.Products.Any())
        {
            ViewBag.Error = "No products found or an error occurred.";
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> OrderPage(string? searchQuery, OrderStatus? selectedStatus, int page = 1,
        int pageSize = 10)
    {
        var viewModel =
            await _adminOrderPageService.GetOrdersPageViewModel(searchQuery, selectedStatus, page, pageSize);

        if (!viewModel.Orders.Any())
        {
            ViewBag.Error = "No orders found or an error occurred.";
        }

        return View(viewModel);
    }

    public IActionResult CustomerPage()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categoryResult = await _categoryServices.GetAllCategories();

        var model = new CreateProductModel
        {
            Categories = categoryResult.Data?.Select(c => c.CategoryName).ToList()
        };

        return View("Forms/CreateProduct", model);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateProductModel model)
    {
        var result = await _adminProductManagementPageService.GetCreateProductViewModel(model);

        if (result.Success)
        {
            TempData["SuccessMessage"] = result.ErrorMessage;
            return RedirectToAction("Create");
        }

        TempData["ErrorMessage"] = result.ErrorMessage;
        return View("Forms/CreateProduct", result.Data);
    }
}
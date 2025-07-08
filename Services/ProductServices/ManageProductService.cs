using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;
using Weav_App.Repositories.Interface;
using Weav_App.Services.General;
using Weav_App.Services.Interface;

namespace Weav_App.Services.ProductServices;

public class ManageProductService : IProductServices
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private Client _supabase;

    public ManageProductService(IProductRepository repository, IMapper mapper, Client supabase)
    {
        _repository = repository;
        _mapper = mapper;
        _supabase = supabase;
    }

    //TODO: To remove this asd check what is the problem here 
    public async Task<ServiceResult<List<ProductDto>>> GetAllProducts()
    {
        var result = await _supabase
            .From<ProductDbTable>()
            .Select("*, Category:CategoryId (CategoryName)")
            .Get();

        var products = result.Models;
    
        var dtoList = _mapper.Map<List<ProductDto>>(products);

        return new ServiceResult<List<ProductDto>>
        {
            Success = true,
            Data = dtoList
        };
    }

    public async Task<ErrorModel> UpdateProduct(ProductDto product)
    {
        var result = await _repository.UpdateProductAsync(_mapper.Map<ProductDbTable>(product));

        return new ErrorModel()
        {
            Message = result.Message,
            Status = result.Status,
        };
    }

    public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(int id)
    {
        var result = await _repository.GetProductByIdAsync(id);
        var dto = _mapper.Map<ProductDto>(result);

        return new ServiceResult<ProductDto>()
        {
            ErrorMessage = "Product was found",
            Success = true,
            Data = dto
        };
    }
    
    public async Task<ErrorModel> DeleteProduct(int id)
    {
        Console.WriteLine($"delete product id: {id}");
        
        var deletedProducts = await _repository.DeleteProduct(id);
        return new ErrorModel()
        {
            Message = deletedProducts.Message,
            Status = deletedProducts.Status,
        };
    }


    public async Task<ServiceResult<List<ProductDto>>> GetLowStokData()
    {
        var dbProducts = await _repository.GetLowStokData();
        var dtoProducts = dbProducts.Select(p => _mapper.Map<ProductDto>(p)).ToList();
        
        return new ServiceResult<List<ProductDto>> { Success = true, Data = dtoProducts };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetInactiveStokData()
    {
        var dbProducts = await _repository.GetInactiveStokData();
        var dtoProducts = dbProducts.Select(p => _mapper.Map<ProductDto>(p)).ToList();
        
        return new ServiceResult<List<ProductDto>> { Success = true, Data = dtoProducts };
    }

    public async Task<ServiceResult<List<ProductDto>>> SearchProductsByCategory(string categoryName)
    {
        var dbProducts = await _repository.GetProductByCategory(categoryName);
        var dtoProducts = dbProducts.Select(p => _mapper.Map<ProductDto>(p)).ToList();

        return new ServiceResult<List<ProductDto>>
        {
            Success = true,
            Data = dtoProducts
        };
    }
}
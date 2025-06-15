using AutoMapper;
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
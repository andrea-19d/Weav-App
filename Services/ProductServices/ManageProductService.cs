using AutoMapper;
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
    
    public ManageProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllProducts()
    {
        var dbProducts = await _repository.GetAllAsync();
        var dtoProducts = dbProducts.Select(p => _mapper.Map<ProductDto>(p)).ToList();

        return new ServiceResult<List<ProductDto>> { Success = true, Data = dtoProducts };
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

    public async Task<ServiceResult<CreateProductViewModel>> CreateProduct(CreateProductModel productModel, string selectedCategory)
    {
        var createProduct = _mapper.Map<ProductDto>(productModel);
        var result = await _repository.CreateNewProduct(createProduct, selectedCategory);
        
        return new ServiceResult<CreateProductViewModel>
        {
            Success = true, 
            Data = null,
            ErrorMessage = result.error
        };
    }
}
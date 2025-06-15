using AutoMapper;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Models;
using Weav_App.Repositories.Interface;

namespace Weav_App.Services.General.InsertStrategies;
public class InsertProductStrategy : IStrategy<CreateProductModel>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public InsertProductStrategy(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ServiceResult<CreateProductModel>> CreateItem(CreateProductModel productModel)
    {
        var productDto = _mapper.Map<ProductDto>(productModel);
        var (success, error) = await _repo.CreateNewProduct(productDto, productModel.SelectedCategory);

        return new ServiceResult<CreateProductModel>
        {
            Success = success,
            Data = success ? productModel : null,
            ErrorMessage = error
        };
    }
}

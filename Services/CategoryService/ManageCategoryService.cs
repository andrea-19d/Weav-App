using AutoMapper;
using Weav_App.Models;
using Weav_App.Repositories.Interface;
using Weav_App.Services.General;
using Weav_App.Services.Interface;

public class ManageCategoryService : ICategoryServices
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public ManageCategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ServiceResult<List<CategoryModel>>> GetAllCategories()
    {
        var dbCategories = await _repository.GetCategories();
        var dtoCategories = dbCategories.Select(p => _mapper.Map<CategoryModel>(p)).ToList();
        
        return new ServiceResult<List<CategoryModel>> { Success = true, Data = dtoCategories };
    }
}
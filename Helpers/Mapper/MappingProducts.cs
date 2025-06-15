using AutoMapper;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;

namespace Weav_App.Helpers.Mapper;

public class MappingProducts : Profile
{
    public MappingProducts()
    {
        // Mapping between ProductModel and ProductDTO
        CreateMap<ProductModel, ProductDbTable>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());
        // Mapping between ProductDTO and ProductDbTable 
        CreateMap<ProductDto, ProductDbTable>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());
        // Mapping between ProductDbTable and ProductDto 
        CreateMap<ProductDbTable, ProductDto>()
            .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Category.CategoryName));
        // Mapping between ProductsList and ProductDTO
        CreateMap<CreateProductModel, ProductDto>();
        CreateMap<ProductDto, CreateProductModel>();
        CreateMap<CategoryDbTable, CategoryModel>();
        CreateMap<CreateProductModel , ProductDto>();
        CreateMap<ProductDto, CreateProductModel>();
    }
    
}
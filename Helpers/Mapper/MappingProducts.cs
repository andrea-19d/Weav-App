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
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Category, opt => opt.Ignore());
        // Mapping between ProductDbTable and ProductDto 
        CreateMap<ProductDbTable, ProductDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Category.CategoryName));
        // Mapping between ProductsList and ProductDTO
        CreateMap<CreateProductModel, ProductDto>();
        CreateMap<ProductDto, CreateProductModel>();
        CreateMap<CategoryDbTable, CategoryModel>();
        CreateMap<ProductDbTable, ProductDbTable>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore()) // Never update primary key
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Preserve original
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => 
                    srcMember != null && 
                    !(srcMember is string str && string.IsNullOrWhiteSpace(str)) &&
                    !(srcMember is int i && i == 0) &&
                    !(srcMember is double d && d == 0.0) &&
                    !(srcMember is DateTime dt && dt == default))
            );
    }
    
}
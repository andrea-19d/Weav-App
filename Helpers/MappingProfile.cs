using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.Products;
using Weav_App.DTOs.Entities.User;
using Weav_App.Models;

namespace Weav_App.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping between RegisterUserDTO and UserDbTable
            CreateMap<RegisterUserDto, UserDbTable>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.LastLogin));

            // Mapping between RegisterUser (from UserModel) and RegisterUserDTO
            CreateMap<RegisterUser, RegisterUserDto>()
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            // Mapping between UserDbTable and UserModel
            CreateMap<UserDbTable, UserModel>()
                .ForCtorParam("userName", opt => opt.MapFrom(src => src.Username))
                .ForCtorParam("email", opt => opt.MapFrom(src => src.Email))
                .ForCtorParam("userIP", opt => opt.MapFrom(src => src.UserIp ?? string.Empty))
                .ForCtorParam("registerDate", opt => opt.MapFrom(src => src.RegisterDate))
                .ForCtorParam("lastLogin", opt => opt.MapFrom(src => src.LastLogin ?? DateTime.MinValue))
                .ForCtorParam("password", opt => opt.MapFrom(src => string.Empty)) // password omitted here for security
                .ForCtorParam("userPhoto", opt => opt.MapFrom(src => src.UserPhoto ?? Array.Empty<byte>()))
                .ForCtorParam("level", opt => opt.MapFrom(src => src.Level != null ? src.Level : UserLevel.User));
            
            // Mapping between LoginUserDTO and LogInUser
            CreateMap<LogInUser, LoginUserDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.RememberMe, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));

            // Mapping between UserDbTable and LoginUserDTO
            CreateMap<UserDbTable, LoginUserDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));

            // Mapping between ProductModel and ProductDTO
            CreateMap<ProductModel, ProductDbTable>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryId));
            
            // Mapping between ProductDTO and ProductDbTable 
            CreateMap<ProductDbTable, ProductDto>()
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice.ToString("F2")))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Category.CategoryName)); // null-safe

            
            // Mapping between ProductDbTable and ProductDto 
            CreateMap<ProductDbTable, ProductDto>()
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice.ToString("F2")))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Category.CategoryName));
            
            CreateMap<ProductDbTable, ProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice.ToString("F2")))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null));

            // Mapping between ProductsList and ProductDTO
            CreateMap<ProductModel.ProductsList, ProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ProductImage));

            CreateMap<CategoryDbTable, CategoryModel>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}

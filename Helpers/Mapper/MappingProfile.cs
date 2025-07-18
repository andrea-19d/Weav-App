﻿using AutoMapper;
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

            CreateMap<RegisterUserModel, RegisterUserDto>();
            CreateMap<UserDbTable, UserDbTable>()
                .ForMember(dest => dest.UserId,opt => opt.Ignore())
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => 
                        srcMember != null && 
                        !(srcMember is string str && string.IsNullOrWhiteSpace(str)) &&
                        !(srcMember is int i && i == 0) &&
                        !(srcMember is double d && d == 0.0) &&
                        !(srcMember is DateTime dt && dt == default))
                );
            CreateMap<UserDbTable, UserAccountData>();
            CreateMap<UserAccountData, UserDbTable>();
        }
    }
}

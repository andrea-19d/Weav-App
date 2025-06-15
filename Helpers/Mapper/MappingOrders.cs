using AutoMapper;
using Weav_App.DTOs.Entities.Miscellaneous;
using Weav_App.DTOs.Entities.Orders;
using Weav_App.Models.ViewsModel;

namespace Weav_App.Helpers;

public class MappingOrders : Profile
{
    public MappingOrders()
    {
            CreateMap<OrdersDbTable, OrdersListDTO>()
                .ForMember(dest => dest.PurchaseOrder, opt => opt.MapFrom(src => src.PurchaseOrder))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));

        CreateMap<OrdersListDTO, OrdersDbTable>()
            .ForMember(dest => dest.PurchaseOrder, opt => opt.MapFrom(src => src.PurchaseOrder))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));

        CreateMap<QuickActionsViewModel, QuickActionsDTO>();
        
        CreateMap<QuickActionsDTO, OrdersListDTO>();
    }
}
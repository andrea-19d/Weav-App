using System.Windows.Input;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Weav_App.DTOs.Entities.User;
using Weav_App.Models;
using Weav_App.Repositories.Interface;
using Weav_App.Services.Interface;

namespace Weav_App.Services.General.InsertStrategies;

public class AddAdminStrategy : IStrategy<RegisterUserModel>
{
    private readonly IAuthService _authenticationService;
    private readonly IMapper _mapper;

    public AddAdminStrategy(IAuthService authenticationService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }
    public async Task<ServiceResult<RegisterUserModel>> CreateItem(RegisterUserModel model)
    {
        var modelDto = _mapper.Map<RegisterUserDto>(model);
        var(succes, error)  = await _authenticationService.RegisterAdmin(modelDto);

        if (succes == false)
        {
            return new ServiceResult<RegisterUserModel>
            {
                Success = false,
                Data = succes ? model : null,
                ErrorMessage = error
            };
        }

        return new ServiceResult<RegisterUserModel>
        {
            Success = true,
            Data = succes ? model : null,
            ErrorMessage = error
        };
    }
}
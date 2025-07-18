using AutoMapper;
using Weav_App.Models;
using Weav_App.Repositories.Interface;
using Weav_App.Services.General;
using Weav_App.Services.Interface;

namespace Weav_App.Services.AdminService;

public class AdminAccountPageServiceService : IAdminAccountPageService
{
    private readonly IAdminAccountRepository _adminAccountRepository;
    private readonly IMapper _mapper;

    public AdminAccountPageServiceService(IAdminAccountRepository adminAccountRepository,  IMapper mapper)
    {
        _adminAccountRepository = adminAccountRepository;
        _mapper = mapper;
    }
    
    public async Task<ServiceResult<UserAccountData>> GetAdminUserData(string email)
    {
        var result = await _adminAccountRepository.GetAdminData(email);
        
        var resultModel = _mapper.Map<UserAccountData>(result.Data);
        
        return new ServiceResult<UserAccountData>()
        {
            Data = resultModel,
            ErrorMessage = "Result was found",
            Success = false
        };
    }

    // public Task<ErrorModel> UpdateUserData(UserModel user)
    // {
    //     throw new NotImplementedException();
    // }
    //
    //
    // public async Task<ErrorModel> UpdateUserData(UserAccountData user)
    // {
    //     throw new NotImplementedException();
    // }
    
}
using AutoMapper;
using Supabase;
using Weav_App.DTOs.Entities.User;
using Weav_App.Helpers;
using Weav_App.Models;
using Weav_App.Repositories.Interface;
using Weav_App.Services.General;
using Xunit;

namespace Weav_App.Repositories;

public class AdminAccountRepository : IAdminAccountRepository
{
    private readonly Client _supabase;
    private readonly IMapper _mapper;
    private readonly UsefulChecks  _usefulChecks;

    public AdminAccountRepository(Client supabase, IMapper mapper,  UsefulChecks usefulChecks)
    {
        _supabase = supabase;
        _mapper = mapper;
        _usefulChecks = usefulChecks;
    }

    public async Task<ServiceResult<UserDbTable>> GetAdminData(string email)
    {
        var checkEmailExists = _usefulChecks.RecordExistsAsync<UserDbTable>("Email",  email).Result;
        var checkUserNameExists = _usefulChecks.RecordExistsAsync<UserDbTable>("userName",  email).Result;
        
        checkEmailExists ??= checkUserNameExists;
        
        try
        {
            if (checkEmailExists == null)
            {
                return new ServiceResult<UserDbTable>()
                {
                    Data = checkEmailExists,
                    ErrorMessage = "Email not found",
                    Success = false
                };
            }

            return new ServiceResult<UserDbTable>()
            {
                Data = checkEmailExists,
                ErrorMessage = "Email not found",
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<UserDbTable>()
            {
                Data = checkEmailExists,
                ErrorMessage = ex.Message,
                Success = false
            };
        }
    }

    public async Task<ErrorModel> UpdateAdminData(UserDbTable model)
    {
        var checkEmailExists = _usefulChecks.RecordExistsAsync<UserDbTable>("Email",  model.Email).Result;
        if (checkEmailExists == null)
        {
            return new ErrorModel()
            {
                Message = "Email not found",
                Status = false
            };
        }

        try
        {
            _mapper.Map(model, checkEmailExists);
            var result = await _supabase
                .From<UserDbTable>()
                .Where(u => u.Email == model.Email)
                .Update(checkEmailExists);
            
            Console.WriteLine("User data updated");
            return new ErrorModel()
            {
                Message = "User data updated",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new ErrorModel()
            {
                Message = ex.Message,
                Status = false
            };
        }
    }
    
    
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weav_App.Data;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.User;
using Weav_App.Services.Interface;

namespace Weav_App.Services.UserServices;

public class UserService : IUserService
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        var userEntity = _mapper.Map<UserDbTable>(registerUserDto);

        if (registerUserDto.Username == userEntity.Username)
        {
            return false;
        }
        
        var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        userEntity.UserIP = ip ?? "Unknown";
        userEntity.Level = UserLevel.User;
        
        try
        {
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> LoginUserAsync(LoginUserDTO loginUserDto, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginUserDto.UserName);
        
        if (user == null)
        {
            return false;
        }

        // TO DO: 
        // Verify password (assuming stored password is hashed)
        var isPasswordValid = VerifyPassword(password, user.PasswordHash);
        return isPasswordValid;
    }

    public bool VerifyPassword(string password, string userPasswordHash)
    {
        if (password == userPasswordHash)
        {
            return true;
        }
        
        return false;
    }
}

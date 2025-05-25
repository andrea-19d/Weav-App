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

    public async Task<(bool success, string? error)> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        var userEntity = _mapper.Map<UserDbTable>(registerUserDto);
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == registerUserDto.Username);
        
        if (existingUser != null && existingUser.Username == userEntity.Username)
        {
            return (false, "Already exists such username");
        }
        
        var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        userEntity.UserIP = ip ?? "Unknown";
        userEntity.Level = UserLevel.Admin;
        
        try
        {
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
            return (true, null);
        }
        catch  (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool succes, UserLevel level)> LoginUserAsync(LoginUserDTO loginUserDto, string password)
    {
        var user = await _context.Users
            .Where(u => u.Username == loginUserDto.UserName)
            .Select(u => new { u.Username, u.PasswordHash, u.Level })
            .FirstOrDefaultAsync();
        
        if (user == null)
        {
            Console.WriteLine("user not found ");
            return (false, UserLevel.Guest);
        }

        // TO DO: 
        // Verify password (assuming stored password is hashed)
        var isPasswordValid = VerifyPassword(password, user.PasswordHash);
        if (!isPasswordValid)
            return (false, UserLevel.Guest);
        
        
        Console.WriteLine($"LoginUserAsync: {user.Level}");
        
        return (isPasswordValid, user.Level);
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

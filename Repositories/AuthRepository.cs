using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Supabase.Postgrest;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.User;
using Weav_App.Repositories.Interface;
using Client = Supabase.Client;

namespace Weav_App.Repositories;

public class AuthRepository :  PasswordHasher<string>, IAuthRepository
{
    private readonly Client _supabase;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthRepository(Client supabase, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _supabase = supabase;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(bool success, string? error)> RegisterUserAsync(RegisterUserDto registerUserDto, UserLevel level)
    {
        var userEntity = _mapper.Map<UserDbTable>(registerUserDto);

        var existing = await _supabase
            .From<UserDbTable>()
            .Filter("userName", Constants.Operator.Equals, registerUserDto.Username)
            .Get();

        if (existing.Models.Any())
            return (false, "Already exists such username");

        var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        userEntity.UserIp = ip ?? "Unknown";
        userEntity.Level = level;
        userEntity.RegisterDate = DateTime.UtcNow;
        userEntity.LastLogin = DateTime.UtcNow;
        userEntity.PasswordHash = HashPassword(userEntity.Username, registerUserDto.Password);

        try
        {
            await _supabase.From<UserDbTable>().Insert(userEntity);
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    //TODO: Authentification for the admin with the supabase things 
    public async Task<(bool succes, UserLevel level)> LoginUserAsync(LoginUserDto loginUserDto, string password)
    {
        var result = await _supabase
            .From<UserDbTable>()
            .Filter("userName", Constants.Operator.Equals, loginUserDto.UserName)
            .Get();

        var user = result.Models.FirstOrDefault();
        if (user == null)
        {
            Console.WriteLine("user not found");
            return (false, UserLevel.Guest);
        }

        var isPasswordValid =VerifyHashedPassword(user.Username, user.PasswordHash, password);
        if (isPasswordValid == PasswordVerificationResult.Failed)
            return (false, UserLevel.Guest);

        user.LastLogin = DateTime.UtcNow;
        Console.WriteLine(user.Username);

        try
        {
            await _supabase.From<UserDbTable>().Update(user);
            Console.WriteLine(user.LastLogin);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        Console.WriteLine($"LoginUserAsync: {user.Level}");
        return (true, user.Level);
    }
}
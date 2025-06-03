using AutoMapper;
using Supabase;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.User;
using Weav_App.Services.Interface;

public class AuthService : IAuthService
{
    private readonly Client _supabase;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(Client supabase, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _supabase = supabase;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(bool success, string? error)> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        var userEntity = _mapper.Map<UserDbTable>(registerUserDto);

        var existing = await _supabase
            .From<UserDbTable>()
            .Filter("UserName", Postgrest.Constants.Operator.Equals, registerUserDto.Username)
            .Get();

        if (existing.Models.Any())
            return (false, "Already exists such username");

        var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        userEntity.UserIP = ip ?? "Unknown";
        userEntity.Level = UserLevel.User;
        userEntity.RegisterDate = DateTime.UtcNow;
        userEntity.LastLogin = DateTime.UtcNow;

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

    public async Task<(bool succes, UserLevel level)> LoginUserAsync(LoginUserDTO loginUserDto, string password)
    {
        var result = await _supabase
            .From<UserDbTable>()
            .Filter("UserName", Postgrest.Constants.Operator.Equals, loginUserDto.UserName)
            .Get();

        var user = result.Models.FirstOrDefault();
        if (user == null)
        {
            Console.WriteLine("user not found");
            return (false, UserLevel.Guest);
        }

        var isPasswordValid = VerifyPassword(password, user.PasswordHash);
        if (!isPasswordValid)
            return (false, UserLevel.Guest);

        Console.WriteLine($"LoginUserAsync: {user.Level}");
        return (true, user.Level);
    }

    public bool VerifyPassword(string password, string userPasswordHash)
    {
        return password == userPasswordHash;
    }
}

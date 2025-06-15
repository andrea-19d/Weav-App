using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Supabase;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.User;
using Weav_App.Repositories;
using Weav_App.Repositories.Interface;
using Weav_App.Services.Interface;

public class AuthService : IAuthService
{
   private readonly IAuthRepository _authRepository;

   public AuthService(IAuthRepository authRepository)
   {
      _authRepository = authRepository;
   }

   public async Task<(bool succes, UserLevel level )> LoginUser(LoginUserDto user, string password)
   {
      var loginUser = await _authRepository.LoginUserAsync(user, password);
      return (loginUser.succes, loginUser.level);
   }

   public async Task<(bool succes, string? error)> RegisterUser(RegisterUserDto user)
   {
      var registerUser = await _authRepository.RegisterUserAsync(user, UserLevel.User);
      return (registerUser.success, registerUser.error);
   }

   public async Task<(bool succes, string? error)> RegisterAdmin(RegisterUserDto user)
   {
      var registerUser = await _authRepository.RegisterUserAsync(user, UserLevel.Admin);
      return (registerUser.success, registerUser.error);
   }
   
}

using Supabase;
using Weav_App.DTOs.Entities.User;

namespace Weav_App.Helpers;

public class UserChecks
{
    private readonly Client _supabase;

    public UserChecks(Client supabase)
    {
        _supabase = supabase;
    }
    
    public async Task<UserDbTable?> UserExists<T>(T user)
    {
      var result = await _supabase.From<UserDbTable>()
                .Filter("UserName", Postgrest.Constants.Operator.Equals, user)
                .Get();
      
      return result.Models.FirstOrDefault();
    }

    public bool VerifyPassword(string password, string userPassword)
    {
        return password == userPassword;
    }
    
    
}
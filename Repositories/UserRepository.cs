using Supabase.Postgrest;
using Weav_App.DTOs.Entities.User;
using Weav_App.Repositories.Interface;
using Client = Supabase.Client;

namespace Weav_App.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Client _supabase;
    
    public UserRepository(Client supabase)
    {
        _supabase = supabase;
    }
    
    public async Task<List<UserDbTable>> GetUsersByIds(List<int> userIds)
    {
        if (userIds == null || userIds.Count == 0)
            return new List<UserDbTable>();

        var users = await _supabase
            .From<UserDbTable>()
            .Filter("id", Constants.Operator.In, userIds)
            .Get();

        return users.Models;
    }



}
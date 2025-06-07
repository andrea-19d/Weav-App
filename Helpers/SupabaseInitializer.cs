using DotNetEnv;
using Supabase;

namespace Weav_App.Helpers;

public static class SupabaseInitializer
{
    public static async Task<Client> InitializeSupabaseAsync()
    {
        // Load env variables
        Env.Load();

        var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        var supabase = new Client(url, key, options);
        await supabase.InitializeAsync();

        return supabase;
    }
}
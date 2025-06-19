using Microsoft.AspNetCore.Authentication.Cookies;
using Weav_App.Helpers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// Serilog Logging
// --------------------------------------------------
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/weavapp-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// --------------------------------------------------
// Supabase Client Initialization
// --------------------------------------------------
var supabase = await SupabaseInitializer.InitializeSupabaseAsync();
builder.Services.AddSingleton(supabase);

// --------------------------------------------------
// Dependency Injection
// --------------------------------------------------
builder.Services.RegisterApplicationServices();

// --------------------------------------------------
// MVC, Razor & HttpContext
// --------------------------------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

// --------------------------------------------------
// Cookie Authentication Setup
// --------------------------------------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Unauthorized";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
    });

// --------------------------------------------------
// Build & Middleware
// --------------------------------------------------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
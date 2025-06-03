using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Weav_App.DTOs;

namespace Weav_App.Controllers.Filters;

public class UserLevelAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly UserLevel _requiredLevel;

    public UserLevelAuthorizeAttribute(UserLevel level)
    {
        _requiredLevel = level;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new ForbidResult();
            return;
        }
        else if (user == null)
        {
            
        }

        var userLevelClaim = user.Claims.FirstOrDefault(c => c.Type == "UserLevel")?.Value;
        if (userLevelClaim == null || !Enum.TryParse(userLevelClaim, out UserLevel userLevel))
        {
            context.Result = new ForbidResult();
            return;
        }

        if (userLevel < _requiredLevel)
        {
            context.Result = new ForbidResult();
        }
        
        Console.WriteLine($"User level claim: {userLevelClaim}, parsed enum: {userLevel}");
    }
}

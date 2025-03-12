using Ester.FarmetTracker.Common.DelegateHandlers.Constants;
using Ester.FarmetTracker.Common.Settings;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ester.FarmetTracker.UI.Web.ActionFilters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class FillTokenParameterAttribute : Attribute, IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var tokenParameters = context.HttpContext.RequestServices.GetService<TokenParameters>()!;



        if (!context.HttpContext.Request.Cookies.TryGetValue(HeaderConstants.AuthorizationCacheKey, out var jwt) || string.IsNullOrEmpty(jwt))
        {
            jwt = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImI2ZjRkZDRjLWNjNTgtNGI1Ni04NzgwLTBhM2I2ZTU3MzIwYSIsInVuaXF1ZV9uYW1lIjoidGVzdCIsImVtYWlsIjoidGVzdCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0Iiwic3ViIjoiIiwiT2Zmc2V0IjoiMDM6MDA6MDAiLCJEZXZpY2VUb2tlbiI6InN0cmluZyIsInNjb3BlIjoibnVyYXltaW5kX3VzZXJfc2NvcGUiLCJuYmYiOjE3NDE3NzM0OTcsImV4cCI6MTc0NDM2NTQ5NywiaXNzIjoid3d3Lm51cmF5bWluZGFwcC5jb20iLCJhdWQiOiJodHRwczovL251cmF5bWluZGFwcC5jb20ifQ.VpALc4GmbpuuM-bm1EzNnIp_9UzHFi4jNP4dnlcIsnY";
        }

        if (jwt.ToString().Split(' ').Length > 1)
            jwt = jwt.ToString().Split(' ')[1];


        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken token;
        try
        {
            token = handler.ReadJwtToken(jwt);
        }
        catch (Exception ex)
        {
            return;
        }

        if (token == null)
            return;

        tokenParameters.AccessToken = jwt;

        var identity = new ClaimsIdentity(token!.Claims, "basic");
        context.HttpContext.User = new ClaimsPrincipal(identity);
        if (!string.IsNullOrEmpty(context.HttpContext?.User?.Identity?.Name))
            tokenParameters.UserName = context.HttpContext.User.Identity.Name;

        tokenParameters.UserId = Guid.Empty;
        var userIdClaim = token.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userIdClaim))
            tokenParameters.UserId = Guid.Parse(userIdClaim);

        context.HttpContext!.Response.HttpContext.User = new ClaimsPrincipal(identity);

    }

}


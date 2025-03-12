using Ester.FarmetTracker.Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ester.FarmetTracker.Common.Filters;

public class FillTokenParameterFilter : IEndpointFilter
{
    private const string SUPER_USER_SCOPE = "admin_user_scope";
    private const string REPRESANTATIVE_USER_SCOPE = "represantative_user_scope";

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var tokenParameters = context.HttpContext.RequestServices.GetService<TokenParameters>()!;
        tokenParameters.IpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? " ";

        if (!context.HttpContext.Request.Headers.TryGetValue("authorization", out StringValues jwt))
            return await next(context);



        if (jwt.ToString().Split(' ').Length > 1)
            jwt = jwt.ToString().Split(' ')[1];


        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        if (token == null)
            return await next(context);

        tokenParameters.AccessToken = jwt;

        var identity = new ClaimsIdentity(token!.Claims, "basic");
        context.HttpContext.User = new ClaimsPrincipal(identity);
        if (!string.IsNullOrEmpty(context.HttpContext?.User?.Identity?.Name))
            tokenParameters.UserName = context.HttpContext.User.Identity.Name;

        tokenParameters.UserId = Guid.Empty;
        var userIdClaim = token.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userIdClaim))
            tokenParameters.UserId = Guid.Parse(userIdClaim);

        if (token.Claims.Any(w => w.Type == "scope" && w.Value == SUPER_USER_SCOPE))
        {
            tokenParameters.Roles.Add(UserRole.Admin);
        }
        if (token.Claims.Any(w => w.Type == "scope" && w.Value == REPRESANTATIVE_USER_SCOPE))
        {
            tokenParameters.Roles.Add(UserRole.Representative);
        }


        context.HttpContext!.Response.HttpContext.User = new ClaimsPrincipal(identity);

        return await next(context);
    }
}

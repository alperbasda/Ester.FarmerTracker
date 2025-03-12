using Core.ApiHelpers.JwtHelper.Models;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.UI.Api.ActionFilters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeHandlerAttribute : Attribute, IAuthorizationFilter
{
    private static TokenValidationParameters? _tokenValidationParameter = null;
    private const string SUPER_USER_SCOPE = "nuraymind_admin_user_scope";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var tokenParameters = context.HttpContext.RequestServices.GetService<TokenParameters>()!;
        var lang = context.HttpContext.Request.Headers["Accept-Language"].ToString() ?? "TR";

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues jwt))
            ThrowAuthorizationException(lang);


        if (jwt.ToString().Split(' ').Length > 1)
            jwt = jwt.ToString().Split(' ')[1];

        ValidateToken(context.HttpContext, jwt!, lang);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        if (token == null)
            ThrowAuthorizationException(lang);

        tokenParameters.AccessToken = jwt;

        var identity = new ClaimsIdentity(token!.Claims, "basic");
        context.HttpContext.User = new ClaimsPrincipal(identity);
        if (!string.IsNullOrEmpty(context.HttpContext?.User?.Identity?.Name))
            tokenParameters.UserName = context.HttpContext.User.Identity.Name;

        tokenParameters.UserId = Guid.Empty;
        var userIdClaim = token.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userIdClaim))
            tokenParameters.UserId = Guid.Parse(userIdClaim);

        tokenParameters.IsSuperUser = token.Claims.Any(w => w.Type == "scope" && w.Value == SUPER_USER_SCOPE);


        context.HttpContext!.Response.HttpContext.User = new ClaimsPrincipal(identity);

    }



    private void ThrowAuthorizationException(string lang)
    {
        throw new AuthorizationException("", lang == "TR" ? "Lütfen giriş yapın." : "Please sign in.");
    }

    private void ValidateToken(HttpContext context, string token, string lang)
    {
        try
        {
            SetIfNullTokenValidationParametersOptions(context);
            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, _tokenValidationParameter, out _);
        }
        catch (Exception e)
        {
            ThrowAuthorizationException(lang);
        }
    }

    private void SetIfNullTokenValidationParametersOptions(HttpContext context)
    {
        if (_tokenValidationParameter == null)
        {
            JwtOptions jwtTokenOptions = context.RequestServices.GetRequiredService<JwtOptions>();
            _tokenValidationParameter = new TokenValidationParameters()
            {
                ValidIssuer = jwtTokenOptions!.Issuer,
                ValidAudience = jwtTokenOptions.Audience,
                IssuerSigningKey = CreateSecurityKey(jwtTokenOptions.SecurityKey),
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

    }

    private static SecurityKey CreateSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }

}


using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ester.FarmetTracker.Common.Filters;

public class AuthorizationFilter : IEndpointFilter
{
    private static TokenValidationParameters? _tokenValidationParameter = null;
    private readonly HashSet<string> _scopes;

    public AuthorizationFilter(params string[] scopes)
    {
        _scopes = new HashSet<string>(scopes);
    }

    public AuthorizationFilter()
    {
        _scopes = new HashSet<string>();
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var lang = context.HttpContext.Request.Headers["Accept-Language"].ToString() ?? "TR";

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues jwt))
            ThrowAuthorizationException();


        if (jwt.ToString().Split(' ').Length > 1)
            jwt = jwt.ToString().Split(' ')[1];

        ValidateToken(context.HttpContext, jwt!);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        if (token == null || _scopes.Any() && !token!.Claims.Where(w => w.Type == "scope").Select(w => w.Value).Any(scope => _scopes.Contains(scope)))
            ThrowAuthorizationException();

        return await next(context);
    }

    private void ThrowAuthorizationException()
    {
        throw new AuthorizationException("Lütfen giriş yapın.");
    }

    private void ValidateToken(HttpContext context, string token)
    {
        try
        {
            SetIfNullTokenValidationParametersOptions(context);
            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, _tokenValidationParameter, out _);
        }
        catch (Exception e)
        {
            ThrowAuthorizationException();
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

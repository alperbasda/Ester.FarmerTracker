using Core.ApiHelpers.JwtHelper.Encyption;
using Core.ApiHelpers.JwtHelper.Models;
using Core.Persistence.Dynamic;
using IdentityServer.Application.Features.Auth.Commands.Login;
using IdentityServer.Application.Features.Auth.Commands.RefreshToken;
using IdentityServer.Application.Features.Clients.Queries.ListDynamic;
using IdentityServer.Application.Services.Repositories;
using IdentityServer.Domain.Entities;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace IdentityServer.Application.Helpers;

public class TokenHelper
{
    JwtOptions _jwtOptions;
    IClientDal _clientDal;
    public TokenHelper(JwtOptions jwtOptions, IClientDal clientDal)
    {
        _jwtOptions = jwtOptions;
        _clientDal = clientDal;
    }

    public async Task<RefreshTokenResponse> CreateUserTokenForRefreshToken(User user)
    {

        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiration);

        var securityKey = SecurityKeyHelper.CreateSecurityKey(_jwtOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
             issuer: _jwtOptions.Issuer,
             expires: accessTokenExpiration,
             notBefore: DateTime.UtcNow,
             claims: await GetClaimsByUser(user),
             signingCredentials: signingCredentials,
             audience: _jwtOptions.Audience
             );

        var handler = new JwtSecurityTokenHandler();

        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new RefreshTokenResponse
        {
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenExpiration)
        };

        return tokenDto;
    }

    public async Task<LoginResponse> CreateUserToken(User user)
    {

        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiration);

        var securityKey = SecurityKeyHelper.CreateSecurityKey(_jwtOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
             issuer: _jwtOptions.Issuer,
             expires: accessTokenExpiration,
             notBefore: DateTime.UtcNow,
             claims: await GetClaimsByUser(user),
             signingCredentials: signingCredentials,
             audience: _jwtOptions.Audience
             );

        var handler = new JwtSecurityTokenHandler();

        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new LoginResponse
        {
            Id = user.Id,
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenExpiration)
        };

        return tokenDto;
    }

    private string CreateRefreshToken()
    {
        var numberByte = new Byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }

    private async Task<List<Claim>> GetClaimsByUser(User user)
    {
        var userClientIds = user.Clients != null ? user.Clients.Select(q => q.ClientId).ToList() : new List<Guid>();

        var clients = (await _clientDal.GetListAsync(w => userClientIds.Contains(w.Id), include: w => w.Include(q => q.ApiResources))).Items;

        var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.NormalizedUserName),
            new Claim(JwtRegisteredClaimNames.Email, user.NormalizedUserName),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, string.Join(',',clients.Select(w=>w.ExternalId))),
            new Claim("Offset", user.Offset.ToString()),
            };

        if (user.UserScopes != null && user.UserScopes.Any())
        {
            userList.AddRange(user.UserScopes.Select(x => new Claim("scope", x.Scope!)));
        }

        userList.AddRange(clients.SelectMany(w => w.ApiResources).Where(w => w != null).Select(x => new Claim(JwtRegisteredClaimNames.Aud, x.ApiResource?.Name ?? " ")));

        return userList;
    }

}

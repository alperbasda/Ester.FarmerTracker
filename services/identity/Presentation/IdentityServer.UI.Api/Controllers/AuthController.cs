using Core.ApiHelpers.JwtHelper.Models;
using Core.Persistence.Models.Responses;
using IdentityServer.Application.Features.Auth.Commands.Login;
using IdentityServer.Application.Features.Auth.Commands.LoginThirdParty;
using IdentityServer.Application.Features.Auth.Commands.RefreshToken;
using IdentityServer.Application.Features.UserRefreshTokens.Commands.DeleteByCode;
using IdentityServer.Application.Features.UserRefreshTokens.Commands.DeleteByUserId;
using IdentityServer.UI.Api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.UI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MediatrController
    {
        private readonly TokenParameters _tokenParameters;
        public AuthController(TokenParameters tokenParameters)
        {
            _tokenParameters = tokenParameters;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateToken(LoginCommand command)
        {
            return Ok(Response<LoginResponse>.Success(await base.Mediator.Send(command), 200));
        }

        [HttpPost("tokenwiththirdparty")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateTokenWithThirdParty(LoginThirdPartyCommand command)
        {
            return Ok(Response<LoginResponse>.Success(await base.Mediator.Send(command), 200));
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            return Ok(Response<DeleteByUserIdRefreshTokenResponse>.Success(await base.Mediator.Send(new DeleteByUserIdRefreshTokenCommand { UserId = _tokenParameters.UserId }), 200));
        }

        [HttpGet("revoke/{refreshToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken)
        {
            return Ok(Response<DeleteByCodeUserRefreshTokenResponse>.Success(await Mediator.Send(new DeleteByCodeUserRefreshTokenCommand { Code = refreshToken }), 200));
        }

        [HttpPost("refreshtoken")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenCommand command)
        {
            return Ok(Response<RefreshTokenResponse>.Success(await Mediator.Send(command), 200));
        }

    }
}

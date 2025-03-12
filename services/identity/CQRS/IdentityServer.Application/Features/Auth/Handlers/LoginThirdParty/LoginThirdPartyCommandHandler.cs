using Core.CrossCuttingConcerns.Exceptions.Types;
using IdentityServer.Application.Features.Auth.Commands.Login;
using IdentityServer.Application.Features.Auth.Commands.LoginThirdParty;
using IdentityServer.Application.Features.Auth.Rules;
using IdentityServer.Application.Features.UserPasswords.Queries.GetByUserId;
using IdentityServer.Application.Features.UserRefreshTokens.Commands.Create;
using IdentityServer.Application.Features.UserRefreshTokens.Commands.DeleteByUserId;
using IdentityServer.Application.Helpers;
using IdentityServer.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Features.Auth.Handlers.LoginThirdParty;

public class LoginThirdPartyCommandHandler : IRequestHandler<LoginThirdPartyCommand, LoginResponse>
{
    private readonly IMediator _mediator;
    private readonly IUserDal _userDal;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly TokenHelper _tokenHelper;

    public LoginThirdPartyCommandHandler(AuthBusinessRules authBusinessRules, IMediator mediator, TokenHelper tokenHelper, IUserDal userDal)
    {
        _authBusinessRules = authBusinessRules;
        _mediator = mediator;
        _tokenHelper = tokenHelper;
        _userDal = userDal;
    }


    public async Task<LoginResponse> Handle(LoginThirdPartyCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDal.GetAsync(w => w.NormalizedMailAddress == request.MailAddress.ToLower(),
            include: w => w
                            .Include(q => q.Clients.Where(w => w.DeletedTime == null))
                            .Include(q => q.UserScopes.Where(w => w.DeletedTime == null))
                            .Include(q => q.Roles.Where(w => w.DeletedTime == null)), cancellationToken: cancellationToken, enableTracking: false);

        //Todo Alper Saçma çözüm ama iş görür.
        if (user == null)
        {
            return new LoginResponse
            {
                AccessToken = "",
                RefreshToken = "",
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(-1)
            };
        }

        if (user.DeviceId != null)
        {
            if (request.DeviceId == null || user.DeviceId.ToLower() != request.DeviceId.ToLower())
            {
                throw new BusinessException("Hesabınız başka bir cihaza bağlı olduğundan giriş yapamazsınız.");
            }
        }

        //Device Token değiştiyse yeni deviceToken ile 
        if (user.DeviceToken != request.DeviceToken)
        {
            user.DeviceToken = request.DeviceToken;
            await _userDal.UpdateAsync(user);
        }

        await _authBusinessRules.FillAllScope(user);

        var token = await _tokenHelper.CreateUserToken(user);
        _ = await _mediator.Send(new DeleteByUserIdRefreshTokenCommand { UserId = user.Id });
        _ = await _mediator.Send(new CreateUserRefreshTokenCommand { Code = token.RefreshToken, Expiration = DateTime.UtcNow.AddDays(7), UserId = user.Id });
        return token;

    }
}

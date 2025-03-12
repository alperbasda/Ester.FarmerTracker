using IdentityServer.Application.Features.Auth.Commands.Login;
using MediatR;

namespace IdentityServer.Application.Features.Auth.Commands.LoginThirdParty;

public class LoginThirdPartyCommand : IRequest<LoginResponse>
{
    public string DeviceId { get; set; }
    public string DeviceToken { get; set; }
    public string MailAddress { get; set; }
}

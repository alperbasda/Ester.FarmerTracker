using MediatR;

namespace IdentityServer.Application.Features.UserRefreshTokens.Commands.DeleteByCode;

public class DeleteByCodeUserRefreshTokenCommand : IRequest<DeleteByCodeUserRefreshTokenResponse>
{
    public string Code { get; set; }
}

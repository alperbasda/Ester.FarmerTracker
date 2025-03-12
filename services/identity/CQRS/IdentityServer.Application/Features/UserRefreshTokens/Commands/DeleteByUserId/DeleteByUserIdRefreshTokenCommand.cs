using MediatR;

namespace IdentityServer.Application.Features.UserRefreshTokens.Commands.DeleteByUserId;

public class DeleteByUserIdRefreshTokenCommand : IRequest<DeleteByUserIdRefreshTokenResponse>
{
    public Guid UserId { get; set; }
}

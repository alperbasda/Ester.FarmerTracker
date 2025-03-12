using MediatR;

namespace IdentityServer.Application.Features.Users.Queries.GetByUserName;

public class GetByMailAddressUserQuery : IRequest<GetByMailAddressUserResponse>
{
    public string MailAddress { get; set; }
    public string Language { get; set; }
}

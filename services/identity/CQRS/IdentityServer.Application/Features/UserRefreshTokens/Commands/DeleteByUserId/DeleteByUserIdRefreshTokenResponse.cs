using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Features.UserRefreshTokens.Commands.DeleteByUserId;

public class DeleteByUserIdRefreshTokenResponse
{
    public Guid UserId { get; set; }

}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Features.UserPasswords.Commands.UpdateForUser;

public class UpdateForUserUserPasswordCommand : IRequest<UpdateForUserUserPasswordResponse>
{
    public Guid UserId { get; set; } = Guid.Empty;

    public string NewPassword { get; set; }

    public string OldPassword { get; set; }
}

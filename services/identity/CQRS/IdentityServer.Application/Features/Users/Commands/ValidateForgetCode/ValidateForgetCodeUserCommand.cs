using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Features.Users.Commands.ValidateForgetCode;

public class ValidateForgetCodeUserCommand : IRequest<ValidateForgetCodeUserResponse>
{
    public string MailAddress { get; set; }

    public string Code { get; set; }

    public string Password { get; set; }
}

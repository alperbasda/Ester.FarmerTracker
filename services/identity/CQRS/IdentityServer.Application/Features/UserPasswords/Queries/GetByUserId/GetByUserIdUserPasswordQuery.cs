using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Features.UserPasswords.Queries.GetByUserId;

public class GetByUserIdUserPasswordQuery : IRequest<GetByUserIdUserPasswordResponse>
{
    public Guid UserId { get; set; }

    public string PasswordStr { get; set; }

}

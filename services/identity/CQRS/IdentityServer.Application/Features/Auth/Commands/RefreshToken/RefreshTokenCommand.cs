﻿//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper template tool. 
//---------------------------------------------------------------------------------------
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
{
    public string RefreshToken { get; set; }
}

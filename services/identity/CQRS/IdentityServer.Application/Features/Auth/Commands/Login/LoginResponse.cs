﻿//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper template tool. 
//---------------------------------------------------------------------------------------

namespace IdentityServer.Application.Features.Auth.Commands.Login;

public class LoginResponse
{
    public Guid Id { get; set; }
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiration { get; set; }
}

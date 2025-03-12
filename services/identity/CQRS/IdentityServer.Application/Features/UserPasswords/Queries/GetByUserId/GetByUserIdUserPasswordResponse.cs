namespace IdentityServer.Application.Features.UserPasswords.Queries.GetByUserId;

public class GetByUserIdUserPasswordResponse
{
    public byte[] Password { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;
}

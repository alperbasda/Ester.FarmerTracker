namespace Ester.FarmetTracker.Common.Settings;

public enum UserRole
{
    None,
    Admin,
    Representative,
    User
}
public class TokenParameters
{
    public string[]? ClientIds { get; set; }

    public Guid UserId { get; set; } = Guid.Empty;

    public string UserName { get; set; } = default!;

    public string IpAddress { get; set; } = default!;

    public string AccessToken { get; set; } = default!;

    public List<UserRole> Roles { get; set; } = [UserRole.None, UserRole.Admin];
}


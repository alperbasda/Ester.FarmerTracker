using Ester.FarmetTracker.Common.Extensions;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;

public class CreateUserRequest
{
    public string DeviceId { get; set; } = "unknown";
    public string DeviceToken { get; set; } = "unknown";
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PasswordConfirm { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string MailAddress { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? CompanyName { get; set; }
    public bool AgreeContact { get; set; }
    public bool PolicyApprove { get; set; }
    public string OffsetStr { get; set; } = default!;
    public int Status { get; set; } = 50;
    public Guid? Id { get; set; } = null;
    public TimeSpan? Offset => OffsetStr.ToTimeSpan();
    public string Language { get; set; } = "";
}

public record CreateUserResponse(Guid Id, string DeviceId, string DeviceToken, string UserName, string Password, string FirstName, string LastName, string MailAddress, string? PhoneNumber, string? CompanyName, bool AgreeContact, TimeSpan Offset);

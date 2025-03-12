namespace Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;

public record UpdateUserRequest(Guid Id, string UserName, string? DeviceId, string? DeviceToken, string FirstName, string LastName, string MailAddress, string? PhoneNumber, bool AgreeContact);

namespace Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;

public record GetByIdUserResponse(Guid Id, string UserName, string FirstName, string LastName, string MailAddress, string? DeviceId, string? DeviceToken, string? PhoneNumber, bool AgreeContact);
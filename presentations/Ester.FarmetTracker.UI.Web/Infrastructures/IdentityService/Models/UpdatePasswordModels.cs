namespace Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;

public record UpdatePasswordRequest(string NewPassword, string ConfirmNewPassword, string OldPassword, Guid? UserId = null);

public record UpdatePasswordResponse(Guid UserId);

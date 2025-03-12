namespace Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;

public record ValidateForgotMailRequest(string MailAddress, string Code, string Password, string PasswordConfirm);

public record ValidateForgotMailResponse(string UserName, string MailAddress);

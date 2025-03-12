namespace Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;

public record LoginRequest(string UserName, string Password, string? DeviceId = null, string? DeviceToken = null);

public record LoginWithGoogleRequest(string DeviceId, string MailAddress);


public record TokenResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiration, Guid Id);

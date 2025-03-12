using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;
using Ester.FarmetTracker.Common.Models.Responses;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService;

public interface IIdentityApiClient
{
    Task<Response<GetByIdUserResponse>> Profile();

    Task<Response<TokenResponse>> CreateToken(LoginRequest request);

    Task<Response<TokenResponse>> CreateTokenThirdParty(LoginWithGoogleRequest request);

    Task<Response<TokenResponse>> RefreshToken(string refreshToken);

    Task<Response<NoContentResponse>> RevokeRefreshToken(string refreshToken);

    Task<Response<TokenResponse>> CreateUser(CreateUserRequest command);

    Task<Response<TokenResponse>> UpdateUser(UpdateUserRequest command);

    Task<Response<DeleteProfileResponse>> DeleteProfile();

    Task<Response<UpdatePasswordResponse>> UpdatePassword(UpdatePasswordRequest command);

    Task<Response<ValidateForgotMailResponse>> SendForgotMail(string mailAddress, string language);

    Task<Response<ValidateForgotMailResponse>> ValidateForgotCode(ValidateForgotMailRequest command);
}

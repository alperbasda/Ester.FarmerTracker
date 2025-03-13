using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

public class IdentityApiClient : IIdentityApiClient
{
    private HttpClient _httpClient;
    
    public IdentityApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<GetByIdUserResponse>> Profile()
    {
        var res = await _httpClient.GetAsync($"users/profile");

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<GetByIdUserResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<TokenResponse>> CreateToken(LoginRequest request)
    {
        var data = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var res = await _httpClient.PostAsync("api/Auth", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<TokenResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<TokenResponse>> CreateTokenThirdParty(LoginWithGoogleRequest request)
    {
        var data = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var res = await _httpClient.PostAsync("/api/auth/tokenwiththirdparty", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<TokenResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<TokenResponse>> CreateUser(CreateUserRequest command)
    {
        var data = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var res = await _httpClient.PostAsync("users", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<TokenResponse>>(apiResponse);

        return result;
    }

    public async Task<JToken> GetDropdown(BaseDynamicRequest data, string endpoint = "")
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{endpoint}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(responseContent);
        return obj["data"]!["items"]!;
    }


    public async Task<Response<TokenResponse>> RefreshToken(string refreshToken)
    {
        var data = new StringContent(JsonConvert.SerializeObject(new { RefreshToken = refreshToken }), Encoding.UTF8, "application/json");

        var res = await _httpClient.PostAsync("api/Auth/refreshtoken", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<TokenResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<NoContentResponse>> RevokeRefreshToken(string refreshToken)
    {
        var res = await _httpClient.GetAsync($"api/Auth/revoke/{refreshToken}");

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<NoContentResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<ValidateForgotMailResponse>> SendForgotMail(string mailAddress,string language)
    {
        var data = new StringContent(JsonConvert.SerializeObject(new { MailAddress = mailAddress,Language= language }), Encoding.UTF8, "application/json");

        var res = await _httpClient.PostAsync($"users/sendforgotmail", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<ValidateForgotMailResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<UpdatePasswordResponse>> UpdatePassword(UpdatePasswordRequest command)
    {

        var data = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var res = await _httpClient.PutAsync("userpasswords", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<UpdatePasswordResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<TokenResponse>> UpdateUser(UpdateUserRequest command)
    {
        var data = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var res = await _httpClient.PutAsync("users", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<TokenResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<ValidateForgotMailResponse>> ValidateForgotCode(ValidateForgotMailRequest command)
    {
        var data = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var res = await _httpClient.PostAsync("users/validateforgotcode", data);

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<ValidateForgotMailResponse>>(apiResponse);

        return result;
    }

    public async Task<Response<DeleteProfileResponse>> DeleteProfile()
    {
        var res = await _httpClient.DeleteAsync($"users/deleteprofile");

        var apiResponse = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<DeleteProfileResponse>>(apiResponse);

        return result;
    }

}

using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Newtonsoft.Json.Linq;

namespace Ester.FarmetTracker.UI.Web.Infrastructures._base;

public interface IClientBase
{
    Task<T> GetAsync<T>(string endpoint = "");
    Task<TResponse> PostAsync<TRequest, TResponse>(TRequest data, string endpoint = "");
    Task<JToken> GetDropdown(BaseDynamicRequest data, string endpoint = "");
    Task<TResponse> PutAsync<TRequest, TResponse>(TRequest data, string endpoint = "");
    Task DeleteAsync(string endpoint = "");


}

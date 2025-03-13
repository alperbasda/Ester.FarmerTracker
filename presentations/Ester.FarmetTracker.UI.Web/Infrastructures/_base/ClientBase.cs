using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Ester.FarmetTracker.UI.Web.Infrastructures._base;

public class ClientBase
{
    private readonly HttpClient _httpClient;

    public ClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<T> GetAsync<T>(string endpoint = "")
    {
        var response = await _httpClient.GetAsync($"api{endpoint}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content)!;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest data, string endpoint = "")
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"api{endpoint}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseContent)!;
    }
    public async Task<JToken> GetDropdown(BaseDynamicRequest data, string endpoint = "")
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"api{endpoint}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var obj = JObject.Parse(responseContent);
        return obj["data"]!["items"]!;
    }
    

    public async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest data, string endpoint = "")
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"api{endpoint}", content);

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseContent)!;
    }

    public async Task DeleteAsync(string endpoint = "")
    {
        var response = await _httpClient.DeleteAsync($"api{endpoint}");
        response.EnsureSuccessStatusCode();
    }

}

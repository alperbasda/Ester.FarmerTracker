using Ester.FarmerTracker.FertilizerService.Infrastructures.FieldApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Ester.FarmerTracker.FertilizerService.Infrastructures.FieldApi;

public class FieldService : IFieldService
{
    private readonly HttpClient _httpClient;

    public FieldService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task IncreaseFieldFertilizerAmount(IncreaseFieldFertilizerAmountRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var resadsa = await _httpClient.PutAsync($"api/fields/increasefertilizeramount", content);
    }
}

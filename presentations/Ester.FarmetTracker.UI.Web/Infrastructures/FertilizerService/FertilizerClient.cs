using Ester.FarmetTracker.UI.Web.Infrastructures._base;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService;

public class FertilizerClient(HttpClient httpClient) : ClientBase(httpClient), IFertilizerClient
{
}

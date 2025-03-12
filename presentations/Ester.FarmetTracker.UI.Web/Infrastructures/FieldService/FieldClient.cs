using Ester.FarmetTracker.UI.Web.Infrastructures._base;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;

public class FieldClient(HttpClient httpClient) : ClientBase(httpClient), IFieldClient
{
}

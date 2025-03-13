using Ester.FarmerTracker.FertilizerService.Infrastructures._settings;
using Ester.FarmerTracker.FertilizerService.Infrastructures.FieldApi;
using Ester.FarmetTracker.Common.DelegateHandlers;
namespace Ester.FarmerTracker.FertilizerService.Infrastructures._extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection collection, ExternalServiceEndpoints endpoints)
    {
        collection.AddTransient<InternalApiDelegateHandler>();
        collection.AddHttpClient<IFieldService, FieldService>((client) =>
        {
            client.BaseAddress = new Uri(endpoints.FieldApi);
        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        })
        .AddHttpMessageHandler<InternalApiDelegateHandler>();

        return collection;
    }
}

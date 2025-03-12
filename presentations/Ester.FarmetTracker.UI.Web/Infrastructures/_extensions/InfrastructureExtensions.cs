using Ester.FarmetTracker.UI.Web.Infrastructures._handlers;
using Ester.FarmetTracker.UI.Web.Infrastructures._settings;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService;

namespace Ester.FarmetTracker.UI.Web.Infrastructures._extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection collection, ExternalServiceEndpoints endpoints)
    {
        collection.AddTransient<ApiClientDelegateHandler>();
        collection.AddHttpClient<IFertilizerClient, FertilizerClient>((client) =>
        {
            client.BaseAddress = new Uri(endpoints.FertilizerApi);
        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        })
        .AddHttpMessageHandler<ApiClientDelegateHandler>();

        collection.AddHttpClient<IFieldClient, FieldClient>((client) =>
        {
            client.BaseAddress = new Uri(endpoints.FieldApi);
        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        })
        .AddHttpMessageHandler<ApiClientDelegateHandler>();

        collection.AddHttpClient<IIdentityApiClient, IdentityApiClient>((client) =>
        {
            client.BaseAddress = new Uri(endpoints.IdentityApi);
        }).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        })
        .AddHttpMessageHandler<ApiClientDelegateHandler>();

        return collection;
    }
}

using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories.Create;
using Ester.FarmetTracker.Common.Filters;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base._extensions;

public static class FertilizerHistoryEndpointExtension
{
    public static WebApplication AddFertilizerHistoryEndpoints(this WebApplication app)
    {
        app.MapGroup("api/fertilizerhistories")
            .CreateFertilizerHistoryEndpoint()
            .AddEndpointFilter<FillTokenParameterFilter>();

        return app;
    }
}

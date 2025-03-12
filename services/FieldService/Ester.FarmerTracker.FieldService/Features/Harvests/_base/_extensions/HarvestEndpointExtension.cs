using Ester.FarmetTracker.Common.Filters;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base._extensions;

public static class HarvestEndpointExtension
{
    public static WebApplication AddHarvestEndpoints(this WebApplication app)
    {
        app.MapGroup("api/harvests")
            //.CreateCategoryEndpoint()
            .AddEndpointFilter<FillTokenParameterFilter>();

        return app;
    }
}

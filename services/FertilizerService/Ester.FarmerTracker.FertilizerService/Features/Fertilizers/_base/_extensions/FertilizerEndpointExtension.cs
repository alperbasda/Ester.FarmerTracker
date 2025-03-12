using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Create;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.DeleteById;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.GetById;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.ListDynamic;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Update;
using Ester.FarmetTracker.Common.Filters;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base._extensions;

public static class FertilizerEndpointExtension
{
    public static WebApplication AddFertilizerEndpoints(this WebApplication app)
    {
        app.MapGroup("api/fertilizers")
            .CreateFertilizerEndpoint()
            .UpdateFertilizerEndpoint()
            .DeleteFertilizerEndpoint()
            .GetByIdFertilizerEndpoint()
            .ListDynamicFertilizerEndpoint()
            .AddEndpointFilter<FillTokenParameterFilter>();

        return app;
    }
}

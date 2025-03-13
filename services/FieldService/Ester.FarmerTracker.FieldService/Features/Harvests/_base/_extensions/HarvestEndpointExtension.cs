using Ester.FarmerTracker.FieldService.Features.Harvests.Complete;
using Ester.FarmerTracker.FieldService.Features.Harvests.Create;
using Ester.FarmerTracker.FieldService.Features.Harvests.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Harvests.GetById;
using Ester.FarmerTracker.FieldService.Features.Harvests.GetLast;
using Ester.FarmerTracker.FieldService.Features.Harvests.ListDynamic;
using Ester.FarmetTracker.Common.Filters;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base._extensions;

public static class HarvestEndpointExtension
{
    public static WebApplication AddHarvestEndpoints(this WebApplication app)
    {
        app.MapGroup("api/harvests")
            .CreateHarvestEndpoint()
            .CompleteHarvestEndpoint()
            .DeleteHarvestEndpoint()
            .GetByIdHarvestEndpoint()
            .GetLastHarvestEndpoint()
            .ListDynamicHarvestEndpoint()
            .AddEndpointFilter<FillTokenParameterFilter>();

        return app;
    }
}

using Ester.FarmerTracker.FieldService.Features.Crops.Create;
using Ester.FarmerTracker.FieldService.Features.Crops.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Crops.GetById;
using Ester.FarmerTracker.FieldService.Features.Crops.ListDynamic;
using Ester.FarmerTracker.FieldService.Features.Crops.Update;
using Ester.FarmetTracker.Common.Filters;

namespace Ester.FarmerTracker.FieldService.Features.Crops._base._extensions;

public static class CropEndpointExtension
{
    public static WebApplication AddCropEndpoints(this WebApplication app)
    {
        app.MapGroup("api/crops")
            .CreateCropEndpoint()
            .UpdateCropEndpoint()
            .DeleteCropEndpoint()
            .GetByIdCropEndpoint()
            .ListDynamicCropEndpoint()
            .AddEndpointFilter<FillTokenParameterFilter>();

        return app;
    }
}

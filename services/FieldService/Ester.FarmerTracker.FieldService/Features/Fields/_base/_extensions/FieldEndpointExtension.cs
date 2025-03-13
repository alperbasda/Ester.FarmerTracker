using Ester.FarmerTracker.FieldService.Features.Fields.Create;
using Ester.FarmerTracker.FieldService.Features.Fields.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Fields.GetById;
using Ester.FarmerTracker.FieldService.Features.Fields.ListDynamic;
using Ester.FarmerTracker.FieldService.Features.Fields.Update;
using Ester.FarmerTracker.FieldService.Features.Fields.UpdateFertilizerAmount;
using Ester.FarmetTracker.Common.Filters;

namespace Ester.FarmerTracker.FieldService.Features.Fields._base._extensions;

public static class FieldEndpointExtension
{
    public static WebApplication AddFieldsEndpoints(this WebApplication app)
    {
        app.MapGroup("api/fields")
            .CreateFieldEndpoint()
            .UpdateFieldEndpoint()
            .DeleteFieldEndpoint()
            .GetByIdFieldEndpoint()
            .ListDynamicFieldEndpoint()
            .UpdateFieldFertilizerAmountEndpoint()
            .AddEndpointFilter<FillTokenParameterFilter>();

        return app;
    }
}

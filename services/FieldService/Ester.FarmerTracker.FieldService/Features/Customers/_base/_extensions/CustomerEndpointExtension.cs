using Ester.FarmerTracker.FieldService.Features.Customers.Create;
using Ester.FarmerTracker.FieldService.Features.Customers.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Customers.GetById;
using Ester.FarmerTracker.FieldService.Features.Customers.ListDynamic;
using Ester.FarmerTracker.FieldService.Features.Customers.Update;
using Ester.FarmetTracker.Common.Filters;

namespace Ester.FarmerTracker.FieldService.Features.Customers._base._extensions;

public static class CustomerEndpointExtension
{
    public static WebApplication AddCustomerEndpoints(this WebApplication app)
    {
        app.MapGroup("api/customers")
            .CreateCustomerEndpoint()
            .UpdateCustomerEndpoint()
            .DeleteCustomerEndpoint()
            .GetByIdCustomerEndpoint()
            .ListDynamicCustomerEndpoint()
            .AddEndpointFilter<FillTokenParameterFilter>();

        return app;
    }
}

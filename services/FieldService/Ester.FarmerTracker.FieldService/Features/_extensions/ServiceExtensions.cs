using Alp.MsSqlAdapter.Extensions;
using Alp.MsSqlAdapter.Settings;
using Alp.ServiceExtensions;
using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Repositories;
using Ester.FarmerTracker.FieldService.Repositories.Contexts;
using Ester.FarmetTracker.Common.Settings;
using Microsoft.Extensions.Caching.Distributed;
using System.Reflection;

namespace Ester.FarmerTracker.FieldService.Features._extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddBusinessRules(this IServiceCollection collection)
    {
        collection.AddScoped<TokenParameters>();
        collection.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        return collection;
    }
    public static IServiceCollection AddContext(this IServiceCollection collection, MsSqlSettings settings)
    {
        collection.AddMsSqlDbContext<FieldDbContext>(settings);


        collection.AddScoped<ICropRepository, CropRepository>();
        collection.AddScoped<ICustomerRepository, CustomerRepository>();
        collection.AddScoped<IFieldRepository, FieldRepository>();
        collection.AddScoped<IHarvestRepository, HarvestRepository>();
        return collection;
    }

}

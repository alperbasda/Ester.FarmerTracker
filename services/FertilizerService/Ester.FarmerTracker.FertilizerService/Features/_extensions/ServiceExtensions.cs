using Alp.MongoAdapter.Extensions;
using Alp.MongoAdapter.Settings;
using Alp.ServiceExtensions;
using Ester.FarmerTracker.FertilizerService.Features._base;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Repositories;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmerTracker.FertilizerService.Repositories.Contexts;
using Ester.FarmetTracker.Common.Settings;
using Microsoft.Extensions.Caching.Distributed;
using System.Reflection;

namespace Ester.FarmerTracker.FertilizerService.Features._extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddBusinessRules(this IServiceCollection collection)
    {
        collection.AddScoped<TokenParameters>();
        collection.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        return collection;
    }
    public static IServiceCollection AddContext(this IServiceCollection collection, MongoSettings settings)
    {
        collection.AddMongoDbContext<FertilizerDbContext>(settings);

        collection.AddScoped<IFertilizerRepository, FertilizerRepository>();
        collection.AddScoped<IFertilizerHistoryRepository, FertilizerHistoryRepository>();
        return collection;
    }

}

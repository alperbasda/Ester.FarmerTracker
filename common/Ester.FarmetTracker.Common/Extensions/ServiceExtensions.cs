using Ester.FarmetTracker.Common.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace Ester.FarmetTracker.Common.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMappings(this IServiceCollection collection, Assembly assembly)
    {
        collection.AddAutoMapper(assembly);
        return collection;
    }

    public static IServiceCollection AddFluentValidations(this IServiceCollection collection, Assembly assembly)
    {
        collection.AddFluentValidationAutoValidation();
        collection.AddValidatorsFromAssembly(assembly);
        return collection;
    }

    public static IServiceCollection AddMediatRService(this IServiceCollection collection, Assembly assembly)
    {
        collection.AddScoped<ITaskRunner, TaskRunner>();

        collection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            //configuration.NotificationPublisher = new TaskWhenAllPublisher();
            //configuration.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            //configuration.Lifetime = ServiceLifetime.Scoped;
        });

        return collection;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, CacheSettings cacheSettings)
    {
        services.AddStackExchangeRedisCache(opt =>
        {

            opt.ConfigurationOptions = new ConfigurationOptions
            {
                Password = cacheSettings.Password,
                EndPoints =
                    {
                        { cacheSettings.Host, int.Parse(cacheSettings.Port) }
                    },
            };
        });

        return services;
    }

}


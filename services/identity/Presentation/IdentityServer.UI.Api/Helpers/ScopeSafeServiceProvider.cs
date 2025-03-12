namespace IdentityServer.UI.Api.Helpers;

public class ScopeSafeServiceProvider
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }

}

namespace Ester.FarmetTracker.UI.Web.Infrastructures.KeepAlives.Alive;

public static class KeepAliveEndpointExtension
{
    public static WebApplication AddKeepAliveEndpoint(this WebApplication app)
    {
        app.MapGet("/health", () =>
        {
            return Results.Ok();
        });
        return app;
    }
}

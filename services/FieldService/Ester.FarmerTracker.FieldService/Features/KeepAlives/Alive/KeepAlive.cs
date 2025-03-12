﻿namespace Ester.FarmerTracker.FieldService.Features.KeepAlives.Alive;

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

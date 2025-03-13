using Ester.FarmetTracker.Common.Models.Responses;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ester.FarmetTracker.UI.Web.Middlewares;


public class FileLoggerService
{
    private readonly string _logDirectory;

    public FileLoggerService(string logDirectory)
    {
        _logDirectory = logDirectory;
        Directory.CreateDirectory(_logDirectory);
    }

    public void Log(string message)
    {
        var logFileName = Path.Combine(_logDirectory, $"{DateTime.UtcNow:yyyy-MM-dd}.log");
        var logMessage = $"[{DateTime.UtcNow:HH:mm:ss}] {message}";
        File.AppendAllText(logFileName, logMessage + Environment.NewLine);
    }
}

public static class ExceptionHandlerMiddlewareService
{
    public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection collection, IWebHostEnvironment env)
    {
        var logDirectory = Path.Combine(env.ContentRootPath, "logs");
        Directory.CreateDirectory(logDirectory);
        collection.AddSingleton(new FileLoggerService(logDirectory));
        return collection;
    }
}

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly FileLoggerService _loggerService;

    public ExceptionHandlerMiddleware(RequestDelegate next, FileLoggerService loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context, ITempDataProvider tempDataProvider)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            _loggerService.Log($"Exception: {error.Message}");
            if (IsAjaxRequest(context.Request))
            {
                await context.Response.WriteAsJsonAsync(new Response<string>() { Errors = [error.Message], Data = null });
            }
            else
            {
                var dict = new Dictionary<string, object>
                {
                    { "Error", error.Message }
                };
                tempDataProvider.SaveTempData(context, dict);

                context.Response.Redirect("/");
            }


        }
    }
    private bool IsAjaxRequest(HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
}

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseFileWebExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
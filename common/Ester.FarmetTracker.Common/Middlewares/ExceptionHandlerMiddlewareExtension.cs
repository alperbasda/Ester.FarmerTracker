using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Models.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Ester.FarmetTracker.Common.Middlewares;

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

        for (int i = 0; i < 5; i++)
        {
            try
            {
                using (var fileStream = new FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.None))
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine(logMessage);
                }
                break;
            }
            catch (IOException)
            {
                Thread.Sleep(50);
            }
        }


    }
}

public static class ExceptionHandlerMiddlewareService
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection collection, IWebHostEnvironment env)
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

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException error)
        {
            await context.Response.WriteAsJsonAsync(Response<string>.Fail(error.Message, HttpStatusCode.BadRequest));
        }
        catch (NotFoundException error)
        {
            await context.Response.WriteAsJsonAsync(Response<string>.Fail(error.Message, HttpStatusCode.NotFound));
        }
        catch (AuthorizationException error)
        {
            await context.Response.WriteAsJsonAsync(Response<string>.Fail(error.Message, HttpStatusCode.Unauthorized));
        }
        catch (Exception error)
        {
            _loggerService.Log($"Exception: {error.Message}");
            _loggerService.Log($"StackTrace: {error.StackTrace}");
            await context.Response.WriteAsJsonAsync(Response<string>.Fail(error.Message, HttpStatusCode.InternalServerError));
        }
    }
}

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseFileExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}

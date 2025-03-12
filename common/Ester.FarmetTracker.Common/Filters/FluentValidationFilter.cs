using Ester.FarmetTracker.Common.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Ester.FarmetTracker.Common.Filters;

public class FluentValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null)
        {
            return await next.Invoke(context);
        }

        var arg = context.Arguments.OfType<T>().FirstOrDefault();
        if (arg is null)
        {
            return await next.Invoke(context);
        }

        var validationResult = await validator.ValidateAsync(arg);

        if (!validationResult.IsValid)
        {
            return Response<T>.Fail(string.Join("\n", validationResult.Errors.Select(w => w.ErrorMessage)), HttpStatusCode.BadRequest);
        }

        return await next.Invoke(context);

        //Global yapı
        //var arg = context.Arguments
        //          .Where(x => typeof(IRequest).IsAssignableFrom(x.GetType()))
        //          .FirstOrDefault();

        //if (arg is null)
        //{
        //    return next.Invoke(context);
        //}

        //var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
        //var validator = context.HttpContext.RequestServices.GetService(validatorType);

        //if (validator is null)
        //{
        //    return next.Invoke(context);
        //}

        //var dynamicValidator = (dynamic)validator;

        //var validationResult = await dynamicValidator.ValidateAsync(arg);

        //if (!validationResult.IsValid)
        //{
        //    var errorsProperty = validationResult.GetType().GetProperty("Errors");
        //    var errors = errorsProperty?.GetValue(validationResult) as IEnumerable<dynamic>;

        //    if (errors != null)
        //    {
        //        var errorMessages = errors.Select(w => w.ErrorMessage.ToString());
        //        return Response<T>.Fail(string.Join("\n", errorMessages), HttpStatusCode.BadRequest);
        //    }
        //}

        //return next.Invoke(context);



    }

}

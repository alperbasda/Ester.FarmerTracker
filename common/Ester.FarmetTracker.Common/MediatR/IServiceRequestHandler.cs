using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;

namespace Ester.FarmetTracker.Common.MediatR;

public interface IServiceRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
    where TRequest : IRequest<Response<TResponse>>
{
}

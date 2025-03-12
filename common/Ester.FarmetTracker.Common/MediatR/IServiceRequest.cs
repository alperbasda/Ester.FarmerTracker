using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;

namespace Ester.FarmetTracker.Common.MediatR;

public interface IServiceRequest<T> : IRequest<Response<T>>
{
}

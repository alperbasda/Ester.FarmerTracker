using Ester.FarmetTracker.Common.DelegateHandlers.Constants;
using Microsoft.AspNetCore.Http;

namespace Ester.FarmetTracker.Common.DelegateHandlers;

public class InternalApiDelegateHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InternalApiDelegateHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var headers = _httpContextAccessor.HttpContext?.Request.Headers;
        if (headers != null)
        {
            if (headers.TryGetValue(HeaderConstants.AuthorizationCacheKey, out var token))
            {
                request.Headers.Add(HeaderConstants.AuthorizationCacheKey, token.ToString());
            }
        }

        return base.SendAsync(request, cancellationToken);
    }
}

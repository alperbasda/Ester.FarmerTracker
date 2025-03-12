using Ester.FarmetTracker.Common.DelegateHandlers.Constants;

namespace Ester.FarmetTracker.UI.Web.Infrastructures._handlers;

public class ApiClientDelegateHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiClientDelegateHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var cookies = _httpContextAccessor.HttpContext?.Request.Cookies;
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImI2ZjRkZDRjLWNjNTgtNGI1Ni04NzgwLTBhM2I2ZTU3MzIwYSIsInVuaXF1ZV9uYW1lIjoidGVzdCIsImVtYWlsIjoidGVzdCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0Iiwic3ViIjoiIiwiT2Zmc2V0IjoiMDM6MDA6MDAiLCJEZXZpY2VUb2tlbiI6InN0cmluZyIsInNjb3BlIjoibnVyYXltaW5kX3VzZXJfc2NvcGUiLCJuYmYiOjE3NDE3NzM0OTcsImV4cCI6MTc0NDM2NTQ5NywiaXNzIjoid3d3Lm51cmF5bWluZGFwcC5jb20iLCJhdWQiOiJodHRwczovL251cmF5bWluZGFwcC5jb20ifQ.VpALc4GmbpuuM-bm1EzNnIp_9UzHFi4jNP4dnlcIsnY");
        if (cookies != null)
        {
            if (cookies.TryGetValue(HeaderConstants.AuthorizationCacheKey, out var token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        return base.SendAsync(request, cancellationToken);
    }
}

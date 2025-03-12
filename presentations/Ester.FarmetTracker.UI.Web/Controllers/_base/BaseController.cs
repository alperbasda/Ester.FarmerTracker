using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Extensions;
using Ester.FarmetTracker.UI.Web.Extensions.RedirectExt;
using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Specialized;
using System.Web;

namespace Ester.FarmetTracker.UI.Web.Controllers._base;

public class BaseController : Controller
{
    protected BaseDynamicRequest GetListRequest<TRequest>()
    {
        NameValueCollection collection = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value ?? "");
        var pageRequest = collection.ToPageRequest();
        var dynamicQuery = collection.ToDynamicFilter<TRequest>();
        return new BaseDynamicRequest { DynamicQuery = dynamicQuery, PageRequest = pageRequest };
    }


    protected void ShowErrorIfExists<T>(Response<T> response)
    {
        if ((int)response.StatusCode >= 200 && (int)response.StatusCode <= 299)
            return;
        var asdf = string.Join("<br/>", response.Errors != null ? response.Errors : []);
        throw new BusinessException(asdf);
    }
}

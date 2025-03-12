using Alp.RepositoryAbstraction.Models.Dynamic;
using System.Collections.Specialized;

namespace Ester.FarmetTracker.UI.Web.Extensions;

public static class CollectionToPageRequestExtension
{
    public const string PageQueryStringName = "Page";

    public const string PageSizeQueryStringName = "PageSize";

    public static PageRequest ToPageRequest(this NameValueCollection nvc)
    {
        PageRequest pageRequest = new PageRequest
        {
            PageIndex = 1,
            PageSize = 10
        };
        if (int.TryParse(nvc["Page"], out var result))
        {
            if (result <= 0)
            {
                result = 1;
            }

            pageRequest.PageIndex = result;
        }

        if (int.TryParse(nvc["PageSize"], out var result2))
        {
            pageRequest.PageSize = result2;
        }

        return pageRequest;
    }
}

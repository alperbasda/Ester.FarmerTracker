using Alp.RepositoryAbstraction.Models.Dynamic;
using System.Collections.Specialized;
using System.Reflection;

namespace Ester.FarmetTracker.UI.Web.Extensions;

public static class CollectionToDynamicQueryExtension
{
    public const string SortFieldQueryStringName = "Sort.Field";

    public const string SortOrderOperatorQueryStringName = "Sort.OrderOperator";

    private static string[] ExtractFilters = new string[4] { "Page", "PageSize", "Sort.OrderOperator", "Sort.Field" };

    public static DynamicQuery ToDynamicFilter<T>(this NameValueCollection nvc)
    {
        return new DynamicQuery
        {
            Sort = GetSortInfo(nvc),
            Filter = BuildDynamicFilters<T>(nvc)
        };
    }

    private static Sort? GetSortInfo(NameValueCollection nvc)
    {
        bool flag = false;
        Sort sort = new Sort();
        if (!string.IsNullOrEmpty(nvc["Sort.Field"]))
        {
            sort.Field = nvc["Sort.Field"];
            flag = true;
        }

        if (!string.IsNullOrEmpty(nvc["Sort.OrderOperator"]))
        {
            sort.OrderOperator = ((nvc["Sort.OrderOperator"].ToLower() == "asc") ? OrderOperator.Asc : OrderOperator.Desc);
        }

        return flag ? sort : null;
    }

    private static Filter? BuildDynamicFilters<T>(NameValueCollection nvc)
    {
        Filter filter = null;
        PropertyInfo[] properties = typeof(T).GetProperties();
        List<string> list = nvc.AllKeys.Where((string w) => !ExtractFilters.Contains(w) && w != null).ToList();
        foreach (string key in list)
        {
            bool flag = key.Contains(".");
            PropertyInfo propertyInfo = properties.FirstOrDefault((PropertyInfo w) => w.Name == key);
            if (flag)
            {
                filter = AddOrCreateFilter(filter, Filter.Create(key, FilterOperator.Equals, nvc[key]));
            }
            else
            {
                if (propertyInfo == null || string.IsNullOrEmpty(nvc[key]))
                {
                    continue;
                }

                if (propertyInfo.PropertyType.IsEnum)
                {
                    filter = AddOrCreateFilter(filter, Filter.Create(key, FilterOperator.Equals, nvc[key]));
                    continue;
                }

                switch (Type.GetTypeCode(propertyInfo.PropertyType))
                {
                    case TypeCode.DateTime:
                        {
                            filter = AddOrCreateFilter(filter, Filter.Create(key, FilterOperator.GreaterThanOrEqual, nvc[key]));
                            if (DateTime.TryParse(nvc[key], out var result))
                            {
                                filter = AddOrCreateFilter(filter, Filter.Create(key, FilterOperator.LessThanOrEqual, result.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0)
                                    .ToString()));
                            }

                            break;
                        }
                    case TypeCode.String:
                        filter = AddOrCreateFilter(filter, Filter.Create(key, FilterOperator.Contains, nvc[key]));
                        break;
                    default:
                        filter = AddOrCreateFilter(filter, Filter.Create(key, FilterOperator.Equals, nvc[key]));
                        break;
                }
            }
        }

        return filter;
    }

    private static Filter AddOrCreateFilter(Filter? baseFilter, Filter assignFilter)
    {
        assignFilter = CreateSubFilters(assignFilter);
        if (baseFilter == null)
        {
            return assignFilter;
        }

        if (baseFilter.Filters == null)
        {
            baseFilter.Filters = new List<Filter>();
        }

        baseFilter.Filters.Add(assignFilter);
        return baseFilter;
    }

    private static Filter CreateSubFilters(Filter filter)
    {
        string[] array = filter.Value.Split('|');
        if (array.Length == 1)
        {
            filter.Logic = Logic.And;
            return filter;
        }

        filter.Logic = Logic.Or;
        Filter filter2 = new Filter
        {
            Field = filter.Field,
            Logic = Logic.Or,
            Operator = filter.Operator,
            Value = array[0],
            Filters = new List<Filter>()
        };
        for (int i = 1; i < array.Length; i++)
        {
            filter2.Filters.Add(new Filter
            {
                Field = filter.Field,
                Value = array[i],
                Operator = filter.Operator,
                Logic = Logic.Or
            });
        }

        return filter2;
    }

    public static IList<Filter> GetAllFilters(Filter filter)
    {
        List<Filter> list = new List<Filter>();
        GetFilters(filter, list);
        return list;
    }

    private static void GetFilters(Filter filter, IList<Filter> filters)
    {
        filters.Add(filter);
        if (filter.Filters == null || !filter.Filters.Any())
        {
            return;
        }

        foreach (Filter filter2 in filter.Filters)
        {
            GetFilters(filter2, filters);
        }
    }
}

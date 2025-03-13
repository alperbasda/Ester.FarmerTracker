using Alp.RepositoryAbstraction.Models.Dynamic;
using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("dropdown")]
public class DropdownController(IFieldClient fieldClient, IFertilizerClient fertilizerClient,IIdentityApiClient identityApiClient) : BaseController
{
    [HttpGet("getdata")]
    public async Task<IActionResult> GetDropdownData(string searchTerm, string endpoint, string service, string filterProp,string showProp = "")
    {
        DynamicQuery dynamicQuery = new DynamicQuery
        {
            Filter = searchTerm != null
                ? Filter.Create(filterProp, FilterOperator.ContainsIgnoreCase, searchTerm)
                : Filter.Create(filterProp, FilterOperator.IsEmpty, "")
        };
        var request = new BaseDynamicRequest
        {
            DynamicQuery = dynamicQuery,
            PageRequest = new PageRequest
            {
                PageIndex = 0,
                PageSize = 5
            },
        };
        if (service == "field")
        {
            var data = await fieldClient.GetDropdown(request, endpoint: $"/{endpoint}/list");
            var selectedItems = data.Select(item => new
            {
                id = item["id"],
                text = item[char.ToLower(showProp[0]) + showProp.Substring(1)]
            });

            var json = JsonConvert.SerializeObject(selectedItems);
            return Json(json);

        }
        else if (service == "fertilizer")
        {
            var data = await fertilizerClient.GetDropdown(request, endpoint: $"/{endpoint}/list");
            var selectedItems = data.Select(item => new
            {
                id = item["id"],
                text = item[char.ToLower(showProp[0]) + showProp.Substring(1)]
            });

            var json = JsonConvert.SerializeObject(selectedItems);
            return Json(json);
        }
        else if (service == "identity")
        {
            var data = await identityApiClient.GetDropdown(request, endpoint: $"/{endpoint}/list");
            var selectedItems = data.Select(item => new
            {
                id = item["id"],
                text = item[char.ToLower(showProp[0]) + showProp.Substring(1)]
            });

            var json = JsonConvert.SerializeObject(selectedItems);
            return Json(json);
        }

        return Json("");
    }

    [HttpPost("getdatabyids")]
    public async Task<IActionResult> GetDropdownDataByIds(List<Guid> ids, string endpoint, string service, string filterProp,string showProp ="")
    {
        var dynamicQuery = new DynamicQuery()
        {
            Filter = Filter.Create("Id", FilterOperator.Equals, ids.First().ToString()),
        };
        if (ids.Count > 1)
        {
            dynamicQuery.Filter.Filters = new List<Filter>();
            dynamicQuery.Filter.Logic = Logic.Or;
            for (int i = 1; i < ids.Count; i++)
            {
                dynamicQuery.Filter.Filters.Add(new Filter { Field = "Id", Logic = Logic.Or, Operator = FilterOperator.Equals, Value = ids[i].ToString() });
            }
        }

        var request = new BaseDynamicRequest
        {
            DynamicQuery = dynamicQuery,
            PageRequest = new PageRequest
            {
                PageIndex = 0,
                PageSize = 1000
            },
        };

        if (service == "field")
        {
            var data = await fieldClient.GetDropdown(request, endpoint: $"/{endpoint}/list");
            var selectedItems = data.Select(item => new
            {
                id = item["id"],
                text = item[char.ToLower(showProp[0]) + showProp.Substring(1)]
            });

            var json = JsonConvert.SerializeObject(selectedItems);
            return Json(json);

        }
        else if (service == "fertilizer")
        {
            var data = await fertilizerClient.GetDropdown(request, endpoint: $"/{endpoint}/list");
            var selectedItems = data.Select(item => new
            {
                id = item["id"],
                text = item[char.ToLower(showProp[0]) + showProp.Substring(1)]
            });

            var json = JsonConvert.SerializeObject(selectedItems);
            return Json(json);
        }
        else if (service == "identity")
        {
            var data = await identityApiClient.GetDropdown(request, endpoint: $"/{endpoint}/list");
            var selectedItems = data.Select(item => new
            {
                id = item["id"],
                text = item[char.ToLower(showProp[0]) + showProp.Substring(1)]
            });

            var json = JsonConvert.SerializeObject(selectedItems);
            return Json(json);
        }

        return Json("");
    }
}

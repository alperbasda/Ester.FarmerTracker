using Alp.RepositoryAbstraction.Models.Dynamic;
using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Extensions.RedirectExt;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Fields;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;
using Microsoft.AspNetCore.Mvc;
using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Ester.FarmetTracker.Common.Settings;

namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("field")]
public class FieldController(IFieldClient fieldClient, TokenParameters tokenParameters) : BaseController
{
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var data = await fieldClient.PostAsync<BaseDynamicRequest, Response<ListModel<ListFieldResponse>>>(GetListRequest<MockField>(), endpoint: "/fields/list");
        ShowErrorIfExists(data);

        return View(data.Data);

    }

    [HttpGet("customerfieldspartial")]
    public async Task<IActionResult> CustomerFields(Guid customerId)
    {
        var filters = GetListRequest<MockField>();
        filters.PageRequest.PageSize = 100;
        filters.PageRequest.PageIndex = 0;
        filters.DynamicQuery = new DynamicQuery
        {
            Filter = Filter.Create(nameof(MockField.CustomerId), FilterOperator.Equals, customerId.ToString())
        };

        var data = await fieldClient.PostAsync<BaseDynamicRequest, Response<ListModel<ListFieldResponse>>>(filters, endpoint: "/fields/list");
        ShowErrorIfExists(data);

        return PartialView("Partials/_CustomerFields", data.Data);

    }



    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["TokenParameters"] = tokenParameters;
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateFieldRequest request)
    {
        var res = await fieldClient.PostAsync<CreateFieldRequest, Response<CreateFieldResponse>>(request, endpoint: "/fields");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Field").Success("Tarla Olusturuldu.");
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update(Guid id)
    {
        ViewData["TokenParameters"] = tokenParameters;
        var data = await fieldClient.GetAsync<Response<GetByIdFieldResponse>>(endpoint: $"/fields/{id}");
        ShowErrorIfExists(data);
        return View(data.Data);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateFieldRequest request)
    {
        var res = await fieldClient.PutAsync<UpdateFieldRequest, Response<UpdateFieldResponse>>(request, endpoint: "/fields");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Field").Success("Tarla Düzenlendi.");
    }

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await fieldClient.DeleteAsync(endpoint: $"/fields/{id}");
        return Json("OK");
    }
}


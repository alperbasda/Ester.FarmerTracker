using Alp.RepositoryAbstraction.Models.Dynamic;
using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.Common.Settings;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Extensions.RedirectExt;
using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("fertilizer")]
public class FertilizerController(IFertilizerClient fertilizerClient, TokenParameters parameters) : BaseController
{
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var data = await fertilizerClient.PostAsync<BaseDynamicRequest, Response<ListModel<ListFertilizerResponse>>>(GetListRequest<MockFertilizer>(), endpoint: "/fertilizers/list");
        ShowErrorIfExists(data);

        return View(data.Data);

    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["TokenParameters"] = parameters;
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateFertilizerRequest request)
    {
        var res = await fertilizerClient.PostAsync<CreateFertilizerRequest, Response<CreateFertilizerResponse>>(request, endpoint: "/fertilizers");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Fertilizer").Success("Ekin Olusturuldu.");
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update(Guid id)
    {
        ViewData["TokenParameters"] = parameters;
        var data = await fertilizerClient.GetAsync<Response<GetByIdFertilizerResponse>>(endpoint: $"/fertilizers/{id}");
        ShowErrorIfExists(data);
        return View(data.Data);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateFertilizerRequest request)
    {
        var res = await fertilizerClient.PutAsync<UpdateFertilizerRequest, Response<UpdateFertilizerResponse>>(request, endpoint: "/fertilizers");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Fertilizer").Success("Ekin Düzenlendi.");
    }

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await fertilizerClient.DeleteAsync(endpoint: $"/fertilizers/{id}");
        return Json("OK");
    }

}

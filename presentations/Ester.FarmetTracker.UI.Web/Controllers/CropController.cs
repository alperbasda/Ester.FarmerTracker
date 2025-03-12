using Alp.RepositoryAbstraction.Models.Dynamic;
using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Extensions.RedirectExt;
using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Crops;
using Microsoft.AspNetCore.Mvc;

namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("crop")]
public class CropController(IFieldClient fieldClient) : BaseController
{
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var data = await fieldClient.PostAsync<BaseDynamicRequest, Response<ListModel<ListCropResponse>>>(GetListRequest<MockCrop>(), endpoint: "/crops/list");
        ShowErrorIfExists(data);

        return View(data.Data);

    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCropRequest request)
    {
        var res = await fieldClient.PostAsync<CreateCropRequest, Response<CreateCropResponse>>(request,endpoint: "/crops");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Crop").Success("Ekin Olusturuldu.");
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update(Guid id)
    {
        var data = await fieldClient.GetAsync<Response<GetByIdCropResponse>>(endpoint: $"/crops/{id}");
        ShowErrorIfExists(data);
        return View(data.Data);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateCropRequest request)
    {
        var res = await fieldClient.PutAsync<UpdateCropRequest, Response<UpdateCropResponse>>(request, endpoint: "/crops");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Crop").Success("Ekin Düzenlendi.");
    }

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await fieldClient.DeleteAsync(endpoint: $"/crops/{id}");
        return Json("OK");
    }
}

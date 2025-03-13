using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Extensions.RedirectExt;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.FertilizerHistories;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.FertilizerHistories.Enums;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers;
using Microsoft.AspNetCore.Mvc;
namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("fertilizerhistory")]
public class FertilizerHistoryController(IFertilizerClient fertilizerClient) : BaseController
{

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateFertilizerHistoryRequest request)
    {
        var res = await fertilizerClient.PostAsync<CreateFertilizerHistoryRequest, Response<CreateFertilizerHistoryResponse>>(request, endpoint: "/fertilizerhistories");
        ShowErrorIfExists(res);
        if (request.ActionRequest.Throw != null && Request.Headers["X-Requested-With"] != "XMLHttpRequest")
        {
            return RedirectToAction("Update", "Field", new { Id = request.ActionRequest.Throw.FieldId }).Success("Gübreleme İşlemi Başarılı");
        }
        return Json("OK");
    }

    [HttpGet("createpartial")]
    public IActionResult CreatePartial(Guid fertilizerId)
    {
        ViewData["FertilizerId"] = fertilizerId;
        return PartialView("Partials/_CreatePartial");
    }

    [HttpGet("createdetailpartial")]
    public IActionResult CreateDetailPartial(FertilizerHistoryAction type)
    {
        var model = new CreateFertilizerHistoryRequest(Guid.Empty, "", type, new ActionRequest());
        switch (type)
        {
            case FertilizerHistoryAction.Transfer:
                return PartialView("Partials/_TransferAction", model);
            case FertilizerHistoryAction.Loss:
                return PartialView("Partials/_LossAction", model);
            case FertilizerHistoryAction.Throw:
                return PartialView("Partials/_ThrowAction", model);
            case FertilizerHistoryAction.Unknown:
            default:
                return PartialView("Partials/_UnknownAction", model);
        }
    }

    [HttpGet("throwpartial")]
    public async Task<IActionResult> ThrowPartial(Guid fieldId, Guid customerId)
    {
        var data = await fertilizerClient.GetAsync<Response<List<ListCustomerFertilizerResponse>>>(endpoint: $"/fertilizers/customerfertilizerlist/{customerId}");
        ViewData["Fertilizers"] = data.Data;

        var model = new CreateFertilizerHistoryRequest(Guid.Empty, "", FertilizerHistoryAction.Throw, new ActionRequest() { Throw = new ThrowActionRequest(fieldId, 0) });
        return PartialView("Partials/_ThrowActionForField", model);
    }
}

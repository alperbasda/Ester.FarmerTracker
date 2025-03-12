using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.FertilizerHistories;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.FertilizerHistories.Enums;
using Microsoft.AspNetCore.Mvc;
namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("fertilizerhistory")]
public class FertilizerHistoryController(IFertilizerClient fertilizerClient) : BaseController
{

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateFertilizerHistoryRequest request)
    {
        var res = await fertilizerClient.PostAsync<CreateFertilizerHistoryRequest, Response<CreateFertilizerHistoryResponse>>(request, endpoint: "/fertilizerhistorys");

        return Json(res);
    }

    [HttpGet("createpartial")]
    public IActionResult CreatePartial(Guid fertilizerId)
    {
        ViewData["FertilizerId"] = fertilizerId;
        return PartialView("Partials/_CreatePartial");
    }

    [HttpGet("createdetailpartial")]
    public async Task<IActionResult> CreateDetailPartial(FertilizerHistoryAction type)
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
}

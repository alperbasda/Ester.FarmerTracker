using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Extensions.RedirectExt;
using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Harvests;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;
using Microsoft.AspNetCore.Mvc;

namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("harvest")]
public class HarvestController(IFieldClient fieldClient) : BaseController
{
    [HttpGet("create")]
    public IActionResult CreatePartial(Guid fieldId)
    {
        ViewData["Field"] = fieldId;
        return PartialView("Partials/_CreateHarvest");
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateHarvestRequest request)
    {
        var res = await fieldClient.PostAsync<CreateHarvestRequest, Response<CreateHarvestResponse>>(request, endpoint: "/harvests");
        ShowErrorIfExists(res);
        return RedirectToAction("Update", "Field", new { Id = request.FieldId }).Success("Ekin Kayıt Edildi.");
    }

    [HttpGet("complete")]
    public async Task<IActionResult> Complete(Guid fieldId)
    {
        var res = await fieldClient.GetAsync<Response<CompleteHarvestResponse>>(endpoint: $"/harvests/complete/{fieldId}");
        ShowErrorIfExists(res);
        return RedirectToAction("Update", "Field", new { Id = fieldId }).Success("Hasat Başarılı.");
    }
}

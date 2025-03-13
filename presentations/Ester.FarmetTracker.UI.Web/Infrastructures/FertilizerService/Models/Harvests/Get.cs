namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Harvests;

public record GetByIdHarvestResponse(Guid Id, Guid FieldId, Guid? CropId, DateTime? HarvestTime);

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Harvests;

public record CreateHarvestRequest(Guid FieldId, Guid? CropId, string CropName);

public record CreateHarvestResponse(Guid Id, Guid FieldId, Guid? CropId, string CropName);

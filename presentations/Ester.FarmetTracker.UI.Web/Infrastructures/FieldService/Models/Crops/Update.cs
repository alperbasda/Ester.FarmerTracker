namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Crops;

public record UpdateCropRequest(Guid Id, string Name, string Description);

public record UpdateCropResponse(Guid Id, string Name, string Description);


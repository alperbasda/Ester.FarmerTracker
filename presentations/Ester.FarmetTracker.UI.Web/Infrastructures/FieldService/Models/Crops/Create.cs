namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Crops;

public record CreateCropRequest(string Name, string Description);

public record CreateCropResponse(Guid Id, string Name, string Description);

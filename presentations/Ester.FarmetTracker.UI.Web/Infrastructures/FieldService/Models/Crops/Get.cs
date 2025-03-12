namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Crops;

public record GetByIdCropResponse(Guid Id, string Name, string Description, DateTime CreatedTime, DateTime? UpdatedTime);
public record ListCropResponse(Guid Id, string Name, string Description, DateTime CreatedTime, DateTime? UpdatedTime);


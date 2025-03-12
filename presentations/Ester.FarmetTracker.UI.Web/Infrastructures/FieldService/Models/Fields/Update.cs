namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Fields;

public record UpdateFieldRequest(Guid Id, Guid CustomerId, string? CurrentCropName, decimal CurrentTotalFertilizerAmount, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address);

public record UpdateFieldResponse(Guid Id, Guid CustomerId, string? CurrentCropName, decimal CurrentTotalFertilizerAmount, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address);


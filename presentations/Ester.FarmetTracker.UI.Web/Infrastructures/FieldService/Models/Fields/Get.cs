namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Fields;

public record GetByIdFieldResponse(Guid Id, Guid CustomerId, string Name, string Coordinate, decimal SquareMeter, string? CurrentCropName, decimal CurrentTotalFertilizerAmount, int CityPlate, string City, string Address, DateTime CreatedTime, DateTime UpdatedTime);
public record ListFieldResponse(Guid Id, Guid CustomerId, string Name, string Coordinate, decimal SquareMeter, string? CurrentCropName, decimal CurrentTotalFertilizerAmount, int CityPlate, string City, string Address, DateTime CreatedTime, DateTime UpdatedTime);


namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Fields;

public record CreateFieldRequest(Guid CustomerId, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address);

public record CreateFieldResponse(Guid Id, Guid CustomerId, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address);

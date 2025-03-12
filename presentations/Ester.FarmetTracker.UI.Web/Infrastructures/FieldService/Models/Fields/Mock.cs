namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Fields;

public class MockField
{
    public Guid CustomerId { get; set; }

    public string Name { get; set; }

    public string Coordinate { get; set; }

    public decimal SquareMeter { get; set; }

    public int CityPlate { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

    public string? CurrentCropName { get; set; }

    public decimal CurrentTotalFertilizerAmount { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }
}

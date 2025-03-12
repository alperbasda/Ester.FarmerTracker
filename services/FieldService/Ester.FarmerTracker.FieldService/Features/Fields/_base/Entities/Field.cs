using Alp.MsSqlAdapter.Models;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;

public class Field : Entity
{
    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Coordinate { get; set; } = default!;

    public decimal SquareMeter { get; set; }

    public int CityPlate { get; set; }

    public string City { get; set; } = default!;

    public string Address { get; set; } = default!;

    public string? CurrentCropName { get; set; } = null;

    public decimal CurrentTotalFertilizerAmount { get; set; }

    public ICollection<Harvest> Harvests { get; set; } = default!;
}

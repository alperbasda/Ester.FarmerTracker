using Alp.MsSqlAdapter.Models;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;

public class Crop : Entity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public virtual ICollection<Harvest> Harvests { get; set; } = default!;
}

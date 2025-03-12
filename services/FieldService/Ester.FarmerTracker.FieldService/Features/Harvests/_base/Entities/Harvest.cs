using Alp.MsSqlAdapter.Models;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;

public class Harvest : Entity
{
    public Guid FieldId { get; set; }
     
    public virtual Field Field { get; set; } = default!;

    public Guid? CropId { get; set; }

    public virtual Crop? Crop { get; set; }

    public DateTime? HarvestTime { get; set; }



}

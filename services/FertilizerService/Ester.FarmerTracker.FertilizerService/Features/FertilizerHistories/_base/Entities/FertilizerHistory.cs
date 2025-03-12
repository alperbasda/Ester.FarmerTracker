using Alp.MongoAdapter.Models;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities.Aggregations;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities.Enums;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities;

public class FertilizerHistory : Entity
{
    public Guid FertilizerId { get; set; }

    public string Description { get; set; } = default!;

    public FertilizerHistoryAction Action { get; set; }

    public TransferActionFertilizerHistoryDetail? TransferActionFertilizerHistoryDetail { get; set; } = null;
    public LossActionFertilizerHistoryDetail? LossActionFertilizerHistoryDetail { get; set; } = null;
    public ThrowActionFertilizerHistoryDetail? ThrowActionFertilizerHistoryDetail { get; set; } = null;
}

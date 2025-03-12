namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities.Aggregations;

public class TransferActionFertilizerHistoryDetail
{
    public Guid RecipientId { get; set; }

    public string RecipientName { get; set; } = default!;

    public Guid GiverId { get; set; }

    public string GiverName { get; set; } = default!;
}

public class LossActionFertilizerHistoryDetail
{
    public decimal Amount { get; set; }
}

public class ThrowActionFertilizerHistoryDetail
{
    public Guid FieldId { get; set; }
    public Guid HarvestId { get; set; }
    public decimal Amount { get; set; }
}

using Alp.MongoAdapter.Models;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities.Enums;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities;

public class Fertilizer : Entity
{
    public Guid UserId { get; set; }

    public string UserFullName { get; set; } = default!;

    public string SerialNumber { get; set; } = default!;

    public decimal TotalAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public FertilizerStatus Status { get; set; }

    public DateTime ExpirationTime { get; set; }
}

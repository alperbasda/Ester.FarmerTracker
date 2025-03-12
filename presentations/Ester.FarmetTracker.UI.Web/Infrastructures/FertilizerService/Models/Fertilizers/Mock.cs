using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers.Enums;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers;

public class MockFertilizer
{
    public Guid Id { get; set; }

    public DateTime CreatedTime { get; set; }
    
    public DateTime? UpdatedTime { get; set; }

    public Guid UserId { get; set; }

    public string UserFullName { get; set; }

    public string SerialNumber { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public FertilizerStatus Status { get; set; }

    public DateTime ExpirationTime { get; set; }

}

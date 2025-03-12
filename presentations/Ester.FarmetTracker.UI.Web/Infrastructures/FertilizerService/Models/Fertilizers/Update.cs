using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers.Enums;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers;

public record UpdateFertilizerRequest(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime);

public record UpdateFertilizerResponse(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime);


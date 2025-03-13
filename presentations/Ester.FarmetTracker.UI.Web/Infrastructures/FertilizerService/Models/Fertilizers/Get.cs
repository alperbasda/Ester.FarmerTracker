using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers.Enums;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers;

public record GetByIdFertilizerResponse(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime, DateTime CreatedTime, DateTime? UpdatedTime);
public record ListFertilizerResponse(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime, DateTime CreatedTime, DateTime? UpdatedTime);

public record ListCustomerFertilizerResponse(Guid Id, string Text);
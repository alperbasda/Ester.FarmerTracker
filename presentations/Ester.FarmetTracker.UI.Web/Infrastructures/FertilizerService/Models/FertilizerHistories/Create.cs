using Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.FertilizerHistories.Enums;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.FertilizerHistories;

public record CreateFertilizerHistoryRequest(Guid FertilizerId, string Description, FertilizerHistoryAction Action, ActionRequest ActionRequest);

public class ActionRequest
{
    public TransferActionRequest? Transfer { get; set; }
    public LossActionRequest? Loss { get; set; }
    public ThrowActionRequest? Throw { get; set; }
}

public record TransferActionRequest(Guid RecipientId, string RecipientName, Guid GiverId, string GiverName);

public record LossActionRequest(decimal Amount);

public record ThrowActionRequest(Guid FieldId, decimal Amount);

public record CreateFertilizerHistoryResponse(Guid FertilizerId, string Description, FertilizerHistoryAction Action);

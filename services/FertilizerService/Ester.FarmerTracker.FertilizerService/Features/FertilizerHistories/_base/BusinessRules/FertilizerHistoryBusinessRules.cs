using Ester.FarmerTracker.FertilizerService.Features._base;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories.Create;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmerTracker.FertilizerService.Infrastructures.FieldApi;
using Ester.FarmerTracker.FertilizerService.Infrastructures.FieldApi.Models;
using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Settings;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.BusinessRules;

public class FertilizerHistoryBusinessRules(TokenParameters tokenParameters, IFertilizerRepository fertilizerRepository, IFieldService fieldService) : BaseBusinessRules(tokenParameters)
{
    public async Task ProcessDetail(CreateFertilizerHistoryCommand command)
    {
        var fertilizer = await fertilizerRepository.GetAsync(w => w.Id == command.FertilizerId);
        if (fertilizer == null)
            return;
        if (command.ActionRequest.Loss != null)
        {
            if (fertilizer.RemainingAmount < command.ActionRequest.Loss.Amount)
            {
                throw new BusinessException("Yeterli Gübre Yok");
            }
            fertilizer!.RemainingAmount -= command.ActionRequest.Loss.Amount;
        }
        else if (command.ActionRequest.Transfer != null)
        {
            fertilizer.UserId = command.ActionRequest.Transfer.RecipientId;
            fertilizer.UserFullName = command.ActionRequest.Transfer.RecipientName;
        }
        else if (command.ActionRequest.Throw != null)
        {
            if (fertilizer.RemainingAmount < command.ActionRequest.Throw.Amount)
            {
                throw new BusinessException("Yeterli Gübre Yok");
            }

            fertilizer.RemainingAmount -= command.ActionRequest.Throw.Amount;
            await fieldService.IncreaseFieldFertilizerAmount(new IncreaseFieldFertilizerAmountRequest(command.ActionRequest.Throw.FieldId, command.ActionRequest.Throw.Amount));
        }
        await fertilizerRepository.UpdateAsync(fertilizer);
    }
}

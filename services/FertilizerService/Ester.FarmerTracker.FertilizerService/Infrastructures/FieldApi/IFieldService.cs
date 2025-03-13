using Ester.FarmerTracker.FertilizerService.Infrastructures.FieldApi.Models;

namespace Ester.FarmerTracker.FertilizerService.Infrastructures.FieldApi;

public interface IFieldService
{
    public Task IncreaseFieldFertilizerAmount(IncreaseFieldFertilizerAmountRequest request);
}

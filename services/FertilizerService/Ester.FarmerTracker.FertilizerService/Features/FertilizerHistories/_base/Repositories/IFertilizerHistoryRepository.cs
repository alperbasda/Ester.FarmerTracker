using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Repositories;

public interface IFertilizerHistoryRepository : IAsyncRepository<FertilizerHistory>
{
}

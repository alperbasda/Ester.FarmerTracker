using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities;
using Ester.FarmerTracker.FertilizerService.Repositories.Contexts;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Repositories;

public class FertilizerHistoryRepository : RepositoryBase<FertilizerHistory, FertilizerDbContext>, IFertilizerHistoryRepository
{
    public FertilizerHistoryRepository(FertilizerDbContext context) : base(context)
    {
    }
}

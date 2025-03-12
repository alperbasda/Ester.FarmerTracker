using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities;
using Ester.FarmerTracker.FertilizerService.Repositories.Contexts;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;

public class FertilizerRepository : RepositoryBase<Fertilizer, FertilizerDbContext>, IFertilizerRepository
{
    public FertilizerRepository(FertilizerDbContext context) : base(context)
    {
    }
}

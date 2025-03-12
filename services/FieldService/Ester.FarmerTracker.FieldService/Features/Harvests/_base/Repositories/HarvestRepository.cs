using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;
using Ester.FarmerTracker.FieldService.Repositories.Contexts;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base.Repositories;

public class HarvestRepository : RepositoryBase<Harvest, FieldDbContext>, IHarvestRepository
{
    public HarvestRepository(FieldDbContext context) : base(context)
    {
    }
}

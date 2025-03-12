using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base.Repositories;

public interface IHarvestRepository : IAsyncRepository<Harvest>
{
}

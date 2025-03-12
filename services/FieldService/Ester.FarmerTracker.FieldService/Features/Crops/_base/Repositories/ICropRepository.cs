using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;

public interface ICropRepository : IAsyncRepository<Crop>
{
}

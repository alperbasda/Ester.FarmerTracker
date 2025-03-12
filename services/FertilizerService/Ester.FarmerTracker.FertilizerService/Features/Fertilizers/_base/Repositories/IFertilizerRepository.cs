using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;

public interface IFertilizerRepository : IAsyncRepository<Fertilizer>
{
}

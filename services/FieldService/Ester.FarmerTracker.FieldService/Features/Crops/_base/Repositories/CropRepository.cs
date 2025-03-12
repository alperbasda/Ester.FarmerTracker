using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;
using Ester.FarmerTracker.FieldService.Repositories.Contexts;

namespace Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;

public class CropRepository : RepositoryBase<Crop, FieldDbContext>, ICropRepository
{
    public CropRepository(FieldDbContext context) : base(context)
    {
    }
}

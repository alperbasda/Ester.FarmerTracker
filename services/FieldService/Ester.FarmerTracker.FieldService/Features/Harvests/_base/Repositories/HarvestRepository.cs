using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;
using Ester.FarmerTracker.FieldService.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base.Repositories;

public class HarvestRepository : RepositoryBase<Harvest, FieldDbContext>, IHarvestRepository
{
    public HarvestRepository(FieldDbContext context) : base(context)
    {
    }

    public async Task<Harvest?> GetLastForFieldAsyc(Guid fieldId)
    {
        return await base.Context.Harvests.Where(w => w.FieldId == fieldId).OrderByDescending(r => r.CreatedTime).FirstOrDefaultAsync();
    }
}

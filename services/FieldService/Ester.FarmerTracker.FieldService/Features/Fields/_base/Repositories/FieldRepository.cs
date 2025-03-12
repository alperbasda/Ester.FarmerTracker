using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;
using Ester.FarmerTracker.FieldService.Repositories.Contexts;

namespace Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;

public class FieldRepository : RepositoryBase<Field, FieldDbContext>, IFieldRepository
{
    public FieldRepository(FieldDbContext context) : base(context)
    {
    }
}

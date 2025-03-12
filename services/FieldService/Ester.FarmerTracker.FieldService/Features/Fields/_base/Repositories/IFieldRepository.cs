using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;

public interface IFieldRepository : IAsyncRepository<Field>
{
}

using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;

public interface ICustomerRepository : IAsyncRepository<Customer>
{
}

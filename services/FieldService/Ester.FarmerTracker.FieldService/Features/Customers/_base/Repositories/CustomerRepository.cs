using Alp.RepositoryAbstraction;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;
using Ester.FarmerTracker.FieldService.Repositories.Contexts;

namespace Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;

public class CustomerRepository : RepositoryBase<Customer, FieldDbContext>, ICustomerRepository
{
    public CustomerRepository(FieldDbContext context) : base(context)
    {
    }
}

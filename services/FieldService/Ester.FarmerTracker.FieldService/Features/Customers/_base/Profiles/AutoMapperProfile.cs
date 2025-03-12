using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Customers.Create;
using Ester.FarmerTracker.FieldService.Features.Customers.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Customers.GetById;
using Ester.FarmerTracker.FieldService.Features.Customers.ListDynamic;
using Ester.FarmerTracker.FieldService.Features.Customers.Update;

namespace Ester.FarmerTracker.FieldService.Features.Customers._base.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CreateCustomerResponse>();
        CreateMap<CreateCustomerCommand, Customer>();

        CreateMap<Customer, UpdateCustomerResponse>();
        CreateMap<UpdateCustomerCommand, Customer>();

        CreateMap<Customer, DeleteCustomerResponse>();
        CreateMap<DeleteCustomerCommand, Customer>();

        CreateMap<Customer, GetByIdCustomerResponse>();


        CreateMap<Customer, ListDynamicCustomerResponse>();

        CreateMap<Paginate<Customer>, ListModel<ListDynamicCustomerResponse>>();
    }
}
